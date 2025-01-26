using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace SimpleClinic.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class ReservationController : ControllerBase
{
    public DataAccess.Repository.IRepo<Reservation, int> _repo { get; }
    public DataAccess.Repository.IRepo<DoctorServiceClinic, int> _repoDoctorServiceClinic { get; }

    public ReservationController(IRepo<Reservation,int> repo, IRepo<DoctorServiceClinic, int> repoDoctorServiceClinic)
    {
        _repo = repo;
        _repoDoctorServiceClinic = repoDoctorServiceClinic;
    }

    [HttpGet()]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(List<ReservationModel>))]

    public IActionResult Get()
    {
        List<string> include=new List<string>();
        include.Add("DoctorServiceClinic.Clinic");
        include.Add("DoctorServiceClinic.DoctorService.Doctor");
        include.Add("DoctorServiceClinic.DoctorService.Service");
   
        IQueryable<Reservation> query = _repo.Get(c => 1 == 1,include);

        return Ok(query.Select(c => new ReservationModel(c)).ToList());
    }
  
    [HttpGet]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(int))]
    public IActionResult GetCount()
    {
        IQueryable<Reservation> query = _repo.Get(c => 1 == 1);
        return Ok(query.Count());
    }
    [HttpGet]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(List<DateTime>))]
    public IActionResult GetFreeReservation(int DoctorServiceClinicId)
    {
        List<string> include = new List<string>();
        include.Add("DoctorService");

        List<TimeSpan> freeReservations = new List<TimeSpan>();

        var Reservations = _repo.Get(c => c.DoctorServiceClinicId==DoctorServiceClinicId).ToList();
       

        IQueryable<DoctorServiceClinic> queryDoctorWorkTime = _repoDoctorServiceClinic.Get(c => c.Id == DoctorServiceClinicId, include);
        var activeDoctor = queryDoctorWorkTime.FirstOrDefaultAsync().Result;
        // Get All Reservations
        for (TimeSpan i = activeDoctor.ServiceStartTime.TimeOfDay; i < activeDoctor.ServiceEndTime.TimeOfDay; i += activeDoctor.DoctorService.Period.Value)
        {
            freeReservations.Add(i);

        }
        // exclude taken ones
        if (Reservations !=null ||Reservations.Count!=0)
        {
            foreach (var item in Reservations)
            {

                freeReservations.Remove(item.StartTime.TimeOfDay);
            }
        }
        return Ok(freeReservations);
      
       
    }
    [HttpGet("{id}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(AddReservationModel))]
    public async Task<IActionResult> GetById(int id)
    {
        List<string> include = new List<string>();
        include.Add("DoctorServiceClinic.Clinic");
        include.Add("DoctorServiceClinic.DoctorService.Doctor");
        include.Add("DoctorServiceClinic.DoctorService.Service");

        IQueryable<Reservation> query = _repo.Get(c => c.Id == id, include);

        return Ok(query.Select(c => new ReservationModel(c)).ToList());
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] AddReservationModel reservationModel)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("error");
        }
        
        await _repo.Save(reservationModel.DBReservation);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        return Ok();
    }
    
    //private IQueryable<DoctorService> GetQuery(string filters, string filtertxt)
    //{
    //    List<string> filtelst = new List<string>();
    //    if (!string.IsNullOrEmpty(filters))
    //    {
    //        filtelst = filters.ToLower().Split(';').ToList();
    //    }
    //    IQueryable<DoctorService> query = _repo.Get(c => 1 == 1);
    //    if (filtelst != null && filtelst.Count != 0 && !string.IsNullOrEmpty(filtertxt))
    //    {
    //        foreach (string item in filtelst)
    //        {
    //            switch (item)
    //            {
    //                case "name":
    //                    query = query.Where(c => c.Service.ToLower().Contains(filtertxt.ToLower()));
    //                    break;
                   
    //                default:
    //                    if (item.Trim() != "")
    //                    {
    //                        throw new ArgumentException("The filters parameter should have either name or it is combination separated by ;");
    //                    }
    //                    break;
    //            }
    //        }
    //    }

    //    return query;
    //}
}

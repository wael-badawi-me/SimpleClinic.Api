using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace SimpleClinic.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class DoctorServiceClinicController : ControllerBase
{
    public DataAccess.Repository.IRepo<DoctorServiceClinic, int> _repo { get; }

    public DoctorServiceClinicController(IRepo<DoctorServiceClinic,int> repo)
    {
        _repo = repo;
    }

    [HttpGet()]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(List<DoctorServiceClinicModel>))]

    public IActionResult Get()
    {
      List<string> include = new List<string>();
        include.Add("DoctorService.Doctor");
        include.Add("DoctorService.Service");
        include.Add("Clinic");

        IQueryable<DoctorServiceClinic> query = _repo.Get(c => 1==1,include);
        var res = query.Select(c => new DoctorServiceClinicModel(c)).ToList();
        return Ok(res);
    }
  
    [HttpGet]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(int))]
    public IActionResult GetCount()
    {
        IQueryable<DoctorServiceClinic> query = _repo.Get(c => 1 == 1);
        return Ok(query.Count());
    }
    [HttpGet("{id}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(DoctorServiceClinicModel))]
    public async Task<IActionResult> GetById(int id)
    {
        List<string> include = new List<string>();
        include.Add("DoctorService.Doctor");
        include.Add("DoctorService.Service");
        include.Add("Clinic");

        IQueryable<DoctorServiceClinic> query = _repo.Get(c => c.Id == id, include);
        var res = query.Select(c => new DoctorServiceClinicModel(c)).ToList();
        return Ok(res);
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] AddDoctorServiceClinicModel doctorServiceClinicModel)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("error");
        }
        
        await _repo.Save(doctorServiceClinicModel.DBDoctorServiceClinic);
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

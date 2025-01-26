
using Swashbuckle.AspNetCore.Examples;

namespace Api.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class DoctorController : ControllerBase
{
    public SimpleClinic.DataAccess.Repository.IRepo<Doctor, int> _repo { get; }

    public DoctorController(IRepo<Doctor,int> repo)
    {
        _repo = repo;
    }

    [HttpGet("{skip}/{take}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(List<Doctor>))]
    
    public IActionResult Get(int skip, int take, string filters = "", string filtertxt = "")
    {
        if (skip < 0 || take < 0)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "skip/take parameter value should be a positive integer");
        }
        if (take > 500)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "take parameter value should be a less than 500");
        }
        IQueryable<Doctor> query = GetQuery(filters, filtertxt);
        return Ok(query.Skip(skip).Take(take).Select(c => new DoctorModel(c)).ToList());
    }
  
    [HttpGet]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(int))]
    public IActionResult GetCount(string filters , string filtertxt)
    {
        IQueryable<Doctor> query = GetQuery(filters, filtertxt);
        return Ok(query.Count());
    }
    [HttpGet("{id}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(DoctorModel))]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(new DoctorModel(await _repo.Get(id)));
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] DoctorModel doctor)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("error");
        }
        
        await _repo.Save(doctor.DBDoctor);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        return Ok();
    }
    
    private IQueryable<Doctor> GetQuery(string filters, string filtertxt)
    {
        List<string> filtelst = new List<string>();
        if (!string.IsNullOrEmpty(filters))
        {
            filtelst = filters.ToLower().Split(';').ToList();
        }
        IQueryable<Doctor> query = _repo.Get(c => 1 == 1);
        if (filtelst != null && filtelst.Count != 0 && !string.IsNullOrEmpty(filtertxt))
        {
            foreach (string item in filtelst)
            {
                switch (item)
                {
                    case "name":
                        query = query.Where(c => c.Name.ToLower().Contains(filtertxt.ToLower()));
                        break;
                   
                    default:
                        if (item.Trim() != "")
                        {
                            throw new ArgumentException("The filters parameter should have either name or it is combination separated by ;");
                        }
                        break;
                }
            }
        }

        return query;
    }
}

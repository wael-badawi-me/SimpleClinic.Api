using Swashbuckle.AspNetCore.Filters;

namespace SimpleClinic.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class ClinicController : ControllerBase
{
    public DataAccess.Repository.IRepo<Clinic, int> _repo { get; }

    public ClinicController(IRepo<Clinic,int> repo)
    {
        _repo = repo;
    }

    [HttpGet("{skip}/{take}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(List<Clinic>))]

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
        IQueryable<Clinic> query = GetQuery(filters, filtertxt);
        return Ok(query.Skip(skip).Take(take).Select(c => new ClinicModel(c)).ToList());
    }
  
    [HttpGet]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(int))]
    public IActionResult GetCount()
    {
        IQueryable<Clinic> query = _repo.Get(c => 1 == 1);

        return Ok(query.Count());
    }
    [HttpGet("{id}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ClinicModel))]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(new ClinicModel(await _repo.Get(id)));
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ClinicModel clinic)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("error");
        }
        
        await _repo.Save(clinic.DBClinic);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        return Ok();
    }
    
    private IQueryable<Clinic> GetQuery(string filters, string filtertxt)
    {
        List<string> filtelst = new List<string>();
        if (!string.IsNullOrEmpty(filters))
        {
            filtelst = filters.ToLower().Split(';').ToList();
        }
        IQueryable<Clinic> query = _repo.Get(c => 1 == 1);
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

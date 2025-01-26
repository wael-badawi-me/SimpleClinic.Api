using Swashbuckle.AspNetCore.Filters;

namespace SimpleClinic.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class ServiceController : ControllerBase
{
    public DataAccess.Repository.IRepo<Service, int> _repo { get; }

    public ServiceController(IRepo<Service,int> repo)
    {
        _repo = repo;
    }

    [HttpGet("{skip}/{take}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(List<Service>))]

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
        IQueryable<Service> query = GetQuery(filters, filtertxt);
        return Ok(query.Skip(skip).Take(take).Select(c => new ServiceModel(c)).ToList());
    }
  
    [HttpGet]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(int))]
    public IActionResult GetCount(string filters , string filtertxt)
    {
        IQueryable<Service> query = GetQuery(filters, filtertxt);
        return Ok(query.Count());
    }
    [HttpGet("{id}")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ServiceModel))]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(new ServiceModel(await _repo.Get(id)));
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ServiceModel service)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("error");
        }
        
        await _repo.Save(service.DBService);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        return Ok();
    }
    
    private IQueryable<Service> GetQuery(string filters, string filtertxt)
    {
        List<string> filtelst = new List<string>();
        if (!string.IsNullOrEmpty(filters))
        {
            filtelst = filters.ToLower().Split(';').ToList();
        }
        IQueryable<Service> query = _repo.Get(c => 1 == 1);
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

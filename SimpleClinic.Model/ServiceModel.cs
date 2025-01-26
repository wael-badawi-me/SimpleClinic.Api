namespace SimpleClinic.Model;
public class ServiceModel
{
    private Service service;
    public ServiceModel()
    {
        service = new Service();
    }
    public ServiceModel(Service initService)
    {
        service = initService;
    }
    [JsonIgnore]
    public Service DBService
    {
        get {return service; }
    }
    public int id
    {
        get { return service.Id; }
        set { service.Id = value; }
    }
    [Required]
    public string Name
    {
        get { return service.Name; }
        set { service.Name = value; }
    }
    
}

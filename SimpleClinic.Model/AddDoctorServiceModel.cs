namespace SimpleClinic.Model;
public class AddDoctorServiceModel
{
    private DoctorService doctorService;
    public AddDoctorServiceModel()
    {
        doctorService = new DoctorService();
    }
    public AddDoctorServiceModel(DoctorService initDoctorService)
    {
        doctorService = initDoctorService;
    }
    [JsonIgnore]
    public DoctorService DBDoctorService
    {
        get {return doctorService; }
    }

    public int id
    {
        get { return doctorService.Id; }
        set { doctorService.Id = value; }
    }
    public int? DoctorId
    {
        get { return doctorService.DoctorId; }
        set { doctorService.DoctorId = value; }
    }
    public int? ServiceId
    {
        get { return doctorService.ServiceId; }
        set { doctorService.ServiceId = value; }
    }
    public TimeSpan? Period
    {
        get { return doctorService.Period; }
        set { doctorService.Period = value; }
    }
    

}

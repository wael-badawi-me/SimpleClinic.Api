namespace SimpleClinic.Model;
public class DoctorServiceModel
{
    private DoctorService doctorService;
    public DoctorServiceModel()
    {
        doctorService = new DoctorService();
    }
    public DoctorServiceModel(DoctorService initDoctorService)
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
    public DoctorModel Doctor
    {
        get { return new DoctorModel(doctorService.Doctor) ; }
        set { doctorService.Doctor = value.DBDoctor; }
    }
    public ServiceModel Service
    {
        get { return new ServiceModel(doctorService.Service); }
        set { doctorService.Service = value.DBService; }
    }
    //public int? DoctorId
    //{
    //    get { return doctorService.DoctorId; }
    //    set { doctorService.DoctorId = value; }
    //}
    //public int? ServiceId
    //{
    //    get { return doctorService.ServiceId; }
    //    set { doctorService.ServiceId = value; }
    //}
    public TimeSpan? Period
    {
        get { return doctorService.Period; }
        set { doctorService.Period = value; }
    }
    

}

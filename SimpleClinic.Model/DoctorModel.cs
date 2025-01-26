namespace SimpleClinic.Model;
public class DoctorModel
{
    private Doctor doctor;
    public DoctorModel()
    {
        doctor = new Doctor();
    }
    public DoctorModel(Doctor initDoctor)
    {
        doctor = initDoctor;
    }
    [JsonIgnore]
    public Doctor DBDoctor
    {
        get {return doctor; }
    }
    public int id
    {
        get { return doctor.Id; }
        set { doctor.Id = value; }
    }
    [Required]
    public string Name
    {
        get { return doctor.Name; }
        set { doctor.Name = value; }
    }
    
}

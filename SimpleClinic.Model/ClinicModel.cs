namespace SimpleClinic.Model;
public class ClinicModel
{
    private Clinic clinic;
    public ClinicModel()
    {
        clinic = new Clinic();
    }
    public ClinicModel(Clinic initClinic)
    {
        clinic = initClinic;
    }
    [JsonIgnore]
    public Clinic DBClinic
    {
        get {return clinic; }
    }
    public int id
    {
        get { return clinic.Id; }
        set { clinic.Id = value; }
    }
    [Required]
    public string Name
    {
        get { return clinic.Name; }
        set { clinic.Name = value; }
    }
    public TimeSpan StartWork
    {
        get { return clinic.StartWork; }
        set { clinic.StartWork = value; }
    }


    public TimeSpan EndWork
    {
        get { return clinic.EndWork; }
        set { clinic.EndWork = value; }
    }
}

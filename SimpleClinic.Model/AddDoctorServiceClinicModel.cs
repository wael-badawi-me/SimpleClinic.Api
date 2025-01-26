using SimpleClinic.DataAccess.Models;
using System.Numerics;

namespace SimpleClinic.Model;
public class AddDoctorServiceClinicModel
{
    private DoctorServiceClinic doctorServiceClinic;
    public AddDoctorServiceClinicModel()
    {
        doctorServiceClinic = new DoctorServiceClinic();
    }
    public AddDoctorServiceClinicModel(DoctorServiceClinic initDoctorServiceClinic)
    {
        doctorServiceClinic = initDoctorServiceClinic;
    }
    [JsonIgnore]
    public DoctorServiceClinic DBDoctorServiceClinic
    {
        get {return doctorServiceClinic; }
    }
    public int id
    {
        get { return doctorServiceClinic.Id; }
        set { doctorServiceClinic.Id = value; }
    }
    public int? DoctorServiceId
    {
        get { return doctorServiceClinic.DoctorServiceId; }
        set { doctorServiceClinic.DoctorServiceId = value; }
    }
    public int? ClinicId
    {
        get { return doctorServiceClinic.ClinicId; }
        set { doctorServiceClinic.ClinicId = value; }
    }


    public DateTime ServiceStartTime
    {
        get { return doctorServiceClinic.ServiceStartTime; }
        set { doctorServiceClinic.ServiceStartTime = value; }
    }

    public DateTime ServiceEndTime
    {
        get { return doctorServiceClinic.ServiceEndTime; }
        set { doctorServiceClinic.ServiceEndTime = value; }
    }

    public DateTime? DoctorStartWork
    {
        get { return doctorServiceClinic.DoctorStartWork; }
        set { doctorServiceClinic.DoctorStartWork = value; }
    }

    public DateTime? DoctorEndWork
    {
        get { return doctorServiceClinic.DoctorEndWork; }
        set { doctorServiceClinic.DoctorEndWork = value; }
    }
   
}

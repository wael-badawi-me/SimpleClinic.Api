using SimpleClinic.DataAccess.Models;
using System.Numerics;

namespace SimpleClinic.Model;
public class DoctorServiceClinicModel
{
    private DoctorServiceClinic doctorServiceClinic;
    public DoctorServiceClinicModel()
    {
        doctorServiceClinic = new DoctorServiceClinic();
    }
    public DoctorServiceClinicModel(DoctorServiceClinic initDoctorServiceClinic)
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
    public DoctorServiceModel DoctorService
    {
        get { return new DoctorServiceModel(doctorServiceClinic.DoctorService); }
        set { doctorServiceClinic.DoctorService = value.DBDoctorService; }
    }
    public ClinicModel Clinic
    {
        get { return new ClinicModel(doctorServiceClinic.Clinic); }
        set { doctorServiceClinic.Clinic = value.DBClinic; }
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

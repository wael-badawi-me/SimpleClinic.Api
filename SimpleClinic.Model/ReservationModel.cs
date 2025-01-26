namespace SimpleClinic.Model;
public class ReservationModel
{
    private Reservation reservation;
    public ReservationModel()
    {
        reservation = new Reservation();
    }
    public ReservationModel(Reservation initReservation)
    {
        reservation = initReservation;
    }
    [JsonIgnore]
    public Reservation DBReservation
    {
        get {return reservation; }
    }
    public int id
    {
        get { return reservation.Id; }
        set { reservation.Id = value; }
    }
    public DoctorServiceClinicModel DoctorServiceClinic
    {
        get { return new DoctorServiceClinicModel(reservation.DoctorServiceClinic); }
        set { reservation.DoctorServiceClinic = value.DBDoctorServiceClinic; }
    } 
    public DateTime StartTime
    {
        get { return reservation.StartTime; }
        set { reservation.StartTime = value; }
    } 
    public DateTime EndTime
    {
        get { return reservation.EndTime; }
        set { reservation.EndTime = value; }
    } 
    public bool Status
    {
        get { return reservation.Status; }
        set { reservation.Status = value; }
    }
    
}

namespace SimpleClinic.Model;
public class AddReservationModel
{
    private Reservation reservation;
    public AddReservationModel()
    {
        reservation = new Reservation();
    }
    public AddReservationModel(Reservation initReservation)
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
    public int? DoctorServiceClinicId
    {
        get { return reservation.DoctorServiceClinicId; }
        set { reservation.DoctorServiceClinicId = value; }
    } 
    public DateTime StartTime
    {
        get { return reservation.StartTime; }
        set { reservation.StartTime = value; }
    } 
   
    public bool Status
    {
        get { return reservation.Status; }
        set { reservation.Status = value; }
    }
    
}

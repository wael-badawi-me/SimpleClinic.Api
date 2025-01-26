using SimpleClinic.DataAccess.Models;

namespace SimpleClinic.DataAccess.Repository;
public class ReservationRepo : BaseRepo, IRepo<Reservation, int>
{
    public ReservationRepo(DbContextOptions<ClinicContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public IQueryable<Reservation> Get(System.Linq.Expressions.Expression<Func<Reservation, bool>> predicate, List<string> include = null)
    {
        if (predicate == null)
        {
            predicate = c => 1 == 1;
        }
        IQueryable<Reservation> query = Context.Reservations;
        if (include != null)
        {
            foreach (string item in include)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    query= query.Include(item);
                }
            }
        }
        return query.Where(predicate);
    }
    public Task<Reservation> Get(int Id)
    {
        return Context.Reservations.FirstOrDefaultAsync(c => c.Id == Id);
    }
    public async Task Save(Reservation reservation)
    {
        var result = Context.DoctorServiceClinics.Where(c => c.Id == reservation.DoctorServiceClinicId).Include("DoctorService").FirstOrDefaultAsync().Result;

       var isExist= Context.Reservations.Any(c => c.StartTime == reservation.StartTime);
        if (isExist)
        {
            throw new ArgumentException("Reservation Time was taken");

        }
        if (reservation.StartTime.TimeOfDay<result.ServiceStartTime.TimeOfDay)
        {
            throw new ArgumentException("Reservation Time not valid");

        }
        var endReservation= reservation.StartTime.TimeOfDay + result.DoctorService.Period.Value;
        reservation.EndTime =Convert.ToDateTime(endReservation.ToString());
        if (reservation.Id == 0)
        {
            await Insert(reservation);
        }
        else {
        
           await Update(reservation);
        }
        

    }
    public async Task Update(Reservation reservation)
    {
        try
        {
            Reservation oldReservation = await Get(reservation.Id);
            if (oldReservation == null)
            {
                throw new ArgumentException("Item with provided Id can't be found");
            }


            Context.Entry(await Context.Set<Reservation>().FindAsync(reservation.Id)).State = EntityState.Detached;
            Context.Entry(reservation).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task Insert(Reservation reservation)
    {
        Context.Reservations.Add(reservation);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(int Id)
    {
        Reservation oldReservation = await Get(Id);
        if (oldReservation == null)
        {
            throw new ArgumentException("Item with provided Id can't be found");
        }
        Context.Reservations.Remove(await Get(Id));
        await Context.SaveChangesAsync();
    }


}


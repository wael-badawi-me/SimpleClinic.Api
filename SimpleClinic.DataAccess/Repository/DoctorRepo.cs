namespace SimpleClinic.DataAccess.Repository;
public class DoctorRepo : BaseRepo, IRepo<Doctor, int>
{
    public DoctorRepo(DbContextOptions<ClinicContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public IQueryable<Doctor> Get(System.Linq.Expressions.Expression<Func<Doctor, bool>> predicate, List<string> include = null)
    {
        if (predicate == null)
        {
            predicate = c => 1 == 1;
        }
        IQueryable<Doctor> query = Context.Doctors;
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
    public Task<Doctor> Get(int Id)
    {
        return Context.Doctors.FirstOrDefaultAsync(c => c.Id == Id);
    }
    public async Task Save(Doctor doctor)
    {

        if (doctor.Id == 0)
        {
            await Insert(doctor);
        }
        else {
        
           await Update(doctor);
        }
        

    }
    public async Task Update(Doctor doctor)
    {
        try
        {
            Doctor oldDoctor = await Get(doctor.Id);
            if (oldDoctor == null)
            {
                throw new ArgumentException("Item with provided Id can't be found");
            }


            Context.Entry(await Context.Set<Doctor>().FindAsync(doctor.Id)).State = EntityState.Detached;
            Context.Entry(doctor).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task Insert(Doctor doctor)
    {
        Context.Doctors.Add(doctor);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(int Id)
    {
        Doctor oldDoctor = await Get(Id);
        if (oldDoctor == null)
        {
            throw new ArgumentException("Item with provided Id can't be found");
        }
        Context.Doctors.Remove(await Get(Id));
        await Context.SaveChangesAsync();
    }


}


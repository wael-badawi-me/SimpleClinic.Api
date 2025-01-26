namespace SimpleClinic.DataAccess.Repository;
public class DoctorServiceRepo : BaseRepo, IRepo<DoctorService, int>
{
    public DoctorServiceRepo(DbContextOptions<ClinicContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public IQueryable<DoctorService> Get(System.Linq.Expressions.Expression<Func<DoctorService, bool>> predicate, List<string> include = null)
    {
        if (predicate == null)
        {
            predicate = c => 1 == 1;
        }
        IQueryable<DoctorService> query = Context.DoctorServices;
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
    public Task<DoctorService> Get(int Id)
    {
        return Context.DoctorServices.FirstOrDefaultAsync(c => c.Id == Id);
    }
    public async Task Save(DoctorService doctorService)
    {

        if (doctorService.Id == 0)
        {
            await Insert(doctorService);
        }
        else {
        
           await Update(doctorService);
        }
        

    }
    public async Task Update(DoctorService doctorService)
    {
        try
        {
            DoctorService oldDoctorService = await Get(doctorService.Id);
            if (oldDoctorService == null)
            {
                throw new ArgumentException("Item with provided Id can't be found");
            }


            Context.Entry(await Context.Set<DoctorService>().FindAsync(doctorService.Id)).State = EntityState.Detached;
            Context.Entry(doctorService).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task Insert(DoctorService doctorService)
    {
        Context.DoctorServices.Add(doctorService);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(int Id)
    {
        DoctorService oldDoctorService = await Get(Id);
        if (oldDoctorService == null)
        {
            throw new ArgumentException("Item with provided Id can't be found");
        }
        Context.DoctorServices.Remove(await Get(Id));
        await Context.SaveChangesAsync();
    }


}


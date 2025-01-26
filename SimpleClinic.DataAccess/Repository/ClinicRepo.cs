namespace SimpleClinic.DataAccess.Repository;
public class ClinicRepo : BaseRepo, IRepo<Clinic, int>
{
    public ClinicRepo(DbContextOptions<ClinicContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public IQueryable<Clinic> Get(System.Linq.Expressions.Expression<Func<Clinic, bool>> predicate, List<string> include = null)
    {
        if (predicate == null)
        {
            predicate = c => 1 == 1;
        }
        IQueryable<Clinic> query = Context.Clinics;
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
    public Task<Clinic> Get(int Id)
    {
        return Context.Clinics.FirstOrDefaultAsync(c => c.Id == Id);
    }
    public async Task Save(Clinic clinic)
    {

        if (clinic.Id == 0)
        {
            await Insert(clinic);
        }
        else {
        
           await Update(clinic);
        }
        

    }
    public async Task Update(Clinic clinic)
    {
        try
        {
            Clinic oldClinic = await Get(clinic.Id);
            if (oldClinic == null)
            {
                throw new ArgumentException("Item with provided Id can't be found");
            }


            Context.Entry(await Context.Set<Clinic>().FindAsync(clinic.Id)).State = EntityState.Detached;
            Context.Entry(clinic).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task Insert(Clinic clinic)
    {
        Context.Clinics.Add(clinic);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(int Id)
    {
        Clinic oldClinic = await Get(Id);
        if (oldClinic == null)
        {
            throw new ArgumentException("Item with provided Id can't be found");
        }
        Context.Clinics.Remove(await Get(Id));
        await Context.SaveChangesAsync();
    }


}


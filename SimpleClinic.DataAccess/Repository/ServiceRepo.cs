namespace SimpleClinic.DataAccess.Repository;
public class ServiceRepo : BaseRepo, IRepo<Service, int>
{
    public ServiceRepo(DbContextOptions<ClinicContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public IQueryable<Service> Get(System.Linq.Expressions.Expression<Func<Service, bool>> predicate, List<string> include = null)
    {
        if (predicate == null)
        {
            predicate = c => 1 == 1;
        }
        IQueryable<Service> query = Context.Services;
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
    public Task<Service> Get(int Id)
    {
        return Context.Services.FirstOrDefaultAsync(c => c.Id == Id);
    }
    public async Task Save(Service service)
    {

        if (service.Id == 0)
        {
            await Insert(service);
        }
        else {
        
           await Update(service);
        }
        

    }
    public async Task Update(Service service)
    {
        try
        {
            Service oldService = await Get(service.Id);
            if (oldService == null)
            {
                throw new ArgumentException("Item with provided Id can't be found");
            }


            Context.Entry(await Context.Set<Service>().FindAsync(service.Id)).State = EntityState.Detached;
            Context.Entry(service).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task Insert(Service service)
    {
        Context.Services.Add(service);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(int Id)
    {
        Service oldService = await Get(Id);
        if (oldService == null)
        {
            throw new ArgumentException("Item with provided Id can't be found");
        }
        Context.Services.Remove(await Get(Id));
        await Context.SaveChangesAsync();
    }


}




namespace SimpleClinic.DataAccess.Repository;
public class BaseRepo: IDisposable
{
    public DbContextOptions<ClinicContext> DbContextOptions { get; }
    public BaseRepo(DbContextOptions<ClinicContext> dbContextOptions)
    {
        DbContextOptions = dbContextOptions;
    }
    private ClinicContext context;
    public ClinicContext Context
    {
        get
        {
            if (context == null)
            {
                context = new ClinicContext(DbContextOptions);
            }
            return context;
        }
    }
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
       disposed = true;
    }
    public void Dispose()
    {
        if (context!=null)
        {
            context.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}


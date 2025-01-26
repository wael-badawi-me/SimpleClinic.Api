namespace SimpleClinic.DataAccess.Repository;
public class DoctorServiceClinicRepo : BaseRepo, IRepo<DoctorServiceClinic, int>
{
    public DoctorServiceClinicRepo(DbContextOptions<ClinicContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public IQueryable<DoctorServiceClinic> Get(System.Linq.Expressions.Expression<Func<DoctorServiceClinic, bool>> predicate, List<string> include = null)
    {
        if (predicate == null)
        {
            predicate = c => 1 == 1;
        }
        IQueryable<DoctorServiceClinic> query = Context.DoctorServiceClinics;
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
    public Task<DoctorServiceClinic> Get(int Id)
    {
        return Context.DoctorServiceClinics.FirstOrDefaultAsync(c => c.Id == Id);
    }
    public async Task Save(DoctorServiceClinic doctorServiceClinic)
    {
        var clinic =  Context.Clinics.Where(c => c.Id == doctorServiceClinic.ClinicId).FirstOrDefaultAsync().Result;
        if (doctorServiceClinic.DoctorStartWork.Value.TimeOfDay < clinic.StartWork || doctorServiceClinic.DoctorEndWork.Value.TimeOfDay > clinic.EndWork)
        {
            throw new ArgumentException("Doctor Time must be musured correctly");
        }

        if (doctorServiceClinic.ServiceStartTime.TimeOfDay < doctorServiceClinic.DoctorStartWork.Value.TimeOfDay || doctorServiceClinic.ServiceEndTime.TimeOfDay > doctorServiceClinic.DoctorEndWork.Value.TimeOfDay)
        {
            throw new ArgumentException("Service Time must be musured correctly");
        }



        if (doctorServiceClinic.Id == 0)
        {
            await Insert(doctorServiceClinic);
        }
        else {
        
           await Update(doctorServiceClinic);
        }
        

    }
    public async Task Update(DoctorServiceClinic doctorServiceClinic)
    {
        try
        {
            DoctorServiceClinic oldDoctorServiceClinic = await Get(doctorServiceClinic.Id);
            if (oldDoctorServiceClinic == null)
            {
                throw new ArgumentException("Item with provided Id can't be found");
            }


            Context.Entry(await Context.Set<DoctorServiceClinic>().FindAsync(doctorServiceClinic.Id)).State = EntityState.Detached;
            Context.Entry(doctorServiceClinic).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task Insert(DoctorServiceClinic doctorServiceClinic)
    {
        Context.DoctorServiceClinics.Add(doctorServiceClinic);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(int Id)
    {
        DoctorServiceClinic oldDoctorServiceClinic = await Get(Id);
        if (oldDoctorServiceClinic == null)
        {
            throw new ArgumentException("Item with provided Id can't be found");
        }
        Context.DoctorServiceClinics.Remove(await Get(Id));
        await Context.SaveChangesAsync();
    }


}




using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SimpleClinic.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("simpleClinic");


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ClinicContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("simpleClinic"));
});

builder.Services.AddTransient<IRepo<Doctor, int>, DoctorRepo>();
builder.Services.AddTransient<IRepo<Clinic, int>, ClinicRepo>();
builder.Services.AddTransient<IRepo<Service, int>, ServiceRepo>();
builder.Services.AddTransient<IRepo<DoctorService, int>, DoctorServiceRepo>();
builder.Services.AddTransient<IRepo<DoctorServiceClinic, int>, DoctorServiceClinicRepo>();
builder.Services.AddTransient<IRepo<Reservation, int>, ReservationRepo>();


//builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddControllersWithViews()
//    .AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});

app.UseHttpsRedirection();

//app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

//     





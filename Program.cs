using ElmarakbyTest.Data;
using ElmarakbyTest.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// connect to sql server database
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add Mvc to project
builder.Services.AddMvc();

// Add Runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// register EmployerRepository in dependency injection to be able to get one instance from it through runnig app.
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();




var app = builder.Build();
// enable static files (wwwroot folder)
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();

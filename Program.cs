using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

 //builder.Services.AddDbContext<ComputadoraContext>(options => options.UseInMemoryDatabase("testDB"));
builder.Services.AddSqlServer<ComputadoraContext>(builder.Configuration.GetConnectionString("cnInv"));
var app = builder.Build();


using(var scope=app.Services.CreateScope()){
    var serv=scope.ServiceProvider;
    try
    {
        var contex=serv.GetRequiredService<ComputadoraContext>();
        contex.Database.EnsureCreated();
    }
    catch (System.Exception)
    {
        throw;
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

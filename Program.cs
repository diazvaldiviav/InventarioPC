using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using Microsoft.AspNetCore.Authentication.Cookies;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/User/Access";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Home/Privacy";
    });

//builder.Services.AddDbContext<ComputadoraContext>(options => options.UseInMemoryDatabase("testDB"));
builder.Services.AddSqlServer<ComputadoraContext>(builder.Configuration.GetConnectionString("cnInv"));

var app = builder.Build();


// using(var scope=app.Services.CreateScope()){
//     var serv=scope.ServiceProvider;
//     try
//     {
//         var contex=serv.GetRequiredService<ComputadoraContext>();
//         contex.Database.EnsureCreated();
//     }
//     catch (System.Exception)
//     {
//         throw;
//     }

// }

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Access}/{id?}");



IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env, "../Rotativa");

app.Run();


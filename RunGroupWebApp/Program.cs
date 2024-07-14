using RunGroupWebApp.Data;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroupWebApp.Reposatory;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Services;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IClubReposatory, ClubReposatory>();
builder.Services.AddScoped<IRaceReposatory, RaceReposatory>();
builder.Services.AddScoped<IDashboardReposatory, DashboardReposatory>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IuserReposatory, UserReposatory>();
//add Application DbContex 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

if(args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //Seed.SeedData(app);
    await Seed.SeedUsersAndRolesAsync(app);

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


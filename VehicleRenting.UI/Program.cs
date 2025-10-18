using VehicleRenting.BLL;
using VehicleRenting.DAL.Role;
using VehicleRenting.DAL.User;
using VehicleRenting.DAL.Vehicle;
using VehicleRenting.DAL.VehicleWorkTime;
using VehicleRenting.UI.Mappers.Resolvers;
using AutoMapper;
using VehicleRenting.UI.Mappers;
using VehicleRenting.DAL.Log;
using VehicleRenting.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
 
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<VehicleProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<UserProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<LogProfile>());

builder.Services.AddTransient<LastUpdatedUserResolver>();
builder.Services.AddTransient<UserRoleResolver>();
builder.Services.AddTransient<LogVehicleResolver>();
builder.Services.AddTransient<LogUserResolver>();


builder.Services.AddScoped<IVehicleRepo, VehicleRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IVehicleWorkTimeRepo,VehicleWorkTimeRepo>();
builder.Services.AddScoped<ILogRepo, LogRepo>();
builder.Services.AddScoped<ExcelExportService>();

builder.Services.AddScoped<VehicleManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<LogManager>();
builder.Services.AddScoped<RoleManager>();
builder.Services.AddScoped<VehicleWorkTimeManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

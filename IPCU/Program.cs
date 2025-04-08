using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using IPCU.Data;
using IPCU.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<PatientFormPdfService>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DeviceMonitoringReportService>();
builder.Services.AddScoped<DeviceMonitoringPdfService>();
builder.Services.AddScoped<DeviceMonitoringExcelService>();
builder.Services.AddScoped<DeviceMonitoringYearlyExcelService>();
builder.Services.AddScoped<DeviceMonitoringAreaYearlyExcelService>();
builder.Services.AddScoped<HAIDischargeReportService>();
builder.Services.AddScoped<HAIDischargeExcelService>();
builder.Services.AddScoped<HaiReportService>();
builder.Services.AddScoped<HaiExcelExportService>();



var app = builder.Build();

//// Call this method to seed roles
//await SeedRolesAsync(app.Services);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

//// Method to seed initial roles
//async Task SeedRolesAsync(IServiceProvider serviceProvider)
//{
//    using var scope = serviceProvider.CreateScope();
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    // Define your roles here
//    string[] roleNames = { "Admin", "Doctor", "Nurse", "Patient" };

//    foreach (var roleName in roleNames)
//    {
//        var roleExists = await roleManager.RoleExistsAsync(roleName);
//        if (!roleExists)
//        {
//            await roleManager.CreateAsync(new IdentityRole(roleName));
//        }
//    }
//}
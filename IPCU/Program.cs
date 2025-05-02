using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using IPCU.Data;
using IPCU.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;

// Configure Entity Framework Core with SQL Server

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnotherServerConnection")));

// In Program.cs (for .NET 6+ minimal hosting model)
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PatientConnection"),
        sqlOptions => {
            // Add retry logic which can help with transient errors
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);

            // Set the compatibility level to force use of older query format
            // that doesn't rely as heavily on CTEs
            sqlOptions.CommandTimeout(60);
        }
    )
);

// Also update your ApplicationDbContext configuration similarly
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => {
            sqlOptions.UseRelationalNulls(true);
            // Add this line to ensure proper CTE handling
            sqlOptions.CommandTimeout(60); // Optional: increase timeout
        }
    )
);

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
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddTransient<EmailSender>(); // forgot pw toh







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
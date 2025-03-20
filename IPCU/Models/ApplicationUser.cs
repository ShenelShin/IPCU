using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string EmployeeID { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }  // Nullable
    public string? Initial { get; set; }  // Nullable
    public DateTime? BirthDate { get; set; }  // Nullable
    public string? LicenseNumber { get; set; }  // Nullable
    public DateTime? UpdateDate { get; set; }  // Nullable
    public string? UserRemarks { get; set; }  // Nullable
    public DateTime? UserValidUntil { get; set; }  // Nullable
    public string? UserStatus { get; set; }  // Nullable
    public string? FullName { get; set; }  // Nullable
    public string? DepartmentId { get; set; }  // Nullable
    public string? DefaultDepartment { get; set; }  // Nullable
    public string? SectionID { get; set; }  // Nullable
    public string? HEMOStationID { get; set; }  // Nullable
    public string? AssignedArea { get; set; }
}

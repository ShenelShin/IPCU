using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}

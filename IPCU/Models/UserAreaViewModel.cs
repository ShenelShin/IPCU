using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace IPCU.Models
{
    public class UserAreaViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        [Display(Name = "Assigned Area")]
        public string AssignedArea { get; set; }
    }

    public class AssignAreaViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        [Display(Name = "Assigned Area")]
        public string AssignedArea { get; set; }

        // This will bind to the selected checkboxes
        public List<string> SelectedAreas { get; set; }

        public AssignAreaViewModel()
        {
            SelectedAreas = new List<string>();
        }
    }
}
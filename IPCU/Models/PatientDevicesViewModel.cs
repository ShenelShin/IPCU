using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    // View model for displaying a patient's connected devices
    public class PatientDevicesViewModel
    {
        public PatientViewModel Patient { get; set; }
        public List<DeviceConnected> ConnectedDevices { get; set; }
    }

    // View model for adding a new connected device
    public class AddConnectedDeviceViewModel
    {
        public string IdNum { get; set; }
        public string HospNum { get; set; }
        public string AdmLocation { get; set; }
        public string RoomID { get; set; }
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Device Type")]
        public string DeviceType { get; set; }

        [Required]
        [Display(Name = "Insertion Date")]
        [DataType(DataType.Date)]
        public DateTime DeviceInsert { get; set; }

        [Display(Name = "Removal Date")]
        [DataType(DataType.Date)]
        public DateTime? DeviceRemove { get; set; }
    }

    // View model for updating a connected device
    public class UpdateConnectedDeviceViewModel
    {
        public string DeviceId { get; set; }
        public string IdNum { get; set; }
        public string HospNum { get; set; }
        public string AdmLocation { get; set; }
        public string RoomID { get; set; }
        public string PatientName { get; set; }

        [Display(Name = "Device Type")]
        public string DeviceType { get; set; }

        [Display(Name = "Insertion Date")]
        [DataType(DataType.Date)]
        public DateTime DeviceInsert { get; set; }

        [Required]
        [Display(Name = "Removal Date")]
        [DataType(DataType.Date)]
        public DateTime DeviceRemove { get; set; }
    }
}
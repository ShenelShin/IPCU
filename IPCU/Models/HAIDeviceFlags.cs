using System;
using System.Collections.Generic;
namespace IPCU.Models
{
    public class HAIDeviceFlags
    {
        public bool HasCentralLine { get; set; }
        public bool HasIndwellingUrinaryCatheter { get; set; }
        public bool HasMechanicalVentilator { get; set; }
        // Add more device flags as needed
    }

    public class HAIChecklistViewModel
    {
        public PatientViewModel Patient { get; set; }
        public HAIDeviceFlags DeviceFlags { get; set; }

        // Device-specific checklists and forms
        public Insertion CentralLineInsertionForm { get; set; }
        public List<DailyCentralLineMaintenanceChecklist> CentralLineMaintenanceChecklists { get; set; } = new List<DailyCentralLineMaintenanceChecklist>();

        // Add properties for other device types' checklists as needed

        public Insertion IndwellingUrinaryCatheterInsertion { get; set; }
        public List<DailyCentralLineMaintenanceChecklist> IndwellingUrinaryCatheterMaintenanceChecklists { get; set; }
    }

    // Device type constants for consistency
    public static class DeviceTypes
    {
        public const string CentralLine = "CL";
        public const string IndwellingUrinaryCatheter = "IUC";
        public const string MechanicalVentilator = "MV";
        // Add more device types as needed
    }

    // Infection type mapping to devices
    public static class DeviceInfectionMap
    {
        public static readonly Dictionary<string, List<string>> DeviceToInfections = new Dictionary<string, List<string>>
        {
            { DeviceTypes.CentralLine, new List<string> { "LCBI", "CardiovascularForm" } },
            { DeviceTypes.IndwellingUrinaryCatheter, new List<string> { "UTI" } },
            { DeviceTypes.MechanicalVentilator, new List<string> { "VAE", "PVAE" } }
        };

        // Helper method to get relevant infections for a set of devices
        public static List<string> GetRelevantInfections(IEnumerable<string> deviceTypes)
        {
            var infections = new List<string>();
            foreach (var deviceType in deviceTypes)
            {
                if (DeviceToInfections.ContainsKey(deviceType))
                {
                    infections.AddRange(DeviceToInfections[deviceType]);
                }
            }
            return infections.Distinct().ToList();
        }

        // Helper method to get relevant checklists for a device type
        public static List<string> GetDeviceChecklists(string deviceType)
        {
            var checklists = new List<string>();

            switch (deviceType)
            {
                case DeviceTypes.CentralLine:
                    checklists.AddRange(new[] { "Insertion", "DailyCentralLineMaintenance" });
                    break;
                case DeviceTypes.IndwellingUrinaryCatheter:
                    checklists.AddRange(new[] { "IUCInsertion", "IUCMaintenance" });
                    break;
                case DeviceTypes.MechanicalVentilator:
                    checklists.AddRange(new[] { "VentilatorBundle", "VAEPrevention" });
                    break;
            }

            return checklists;
        }
    }
}
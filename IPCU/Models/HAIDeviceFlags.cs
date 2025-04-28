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
    }

    // Optional: Device type constants for consistency
    public static class DeviceTypes
    {
        public const string CentralLine = "CL";
        public const string IndwellingUrinaryCatheter = "IUC";
        public const string MechanicalVentilator = "MV";
        // Add more device types as needed
    }

    // Optional: Infection type mapping to devices
    public static class DeviceInfectionMap
    {
        public static readonly Dictionary<string, List<string>> DeviceToInfections = new Dictionary<string, List<string>>
        {
            { DeviceTypes.CentralLine, new List<string> { "LCBI" } },
            { DeviceTypes.IndwellingUrinaryCatheter, new List<string> { "UTI" } },
            { DeviceTypes.MechanicalVentilator, new List<string> { "VAE" } }
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
            return infections;
        }
    }
}
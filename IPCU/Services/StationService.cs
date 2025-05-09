using IPCU.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPCU.Services
{
    public class StationService
    {
        private readonly BuildFileDbContext _buildFileContext;

        public StationService(BuildFileDbContext buildFileContext)
        {
            _buildFileContext = buildFileContext;
        }

        public async Task<List<string>> GetAllStationAreas()
        {
            try
            {
                // Add a debug statement to see if we're reaching this point
                System.Diagnostics.Debug.WriteLine("Attempting to get station areas from database");

                // Check if context is available
                if (_buildFileContext == null)
                {
                    System.Diagnostics.Debug.WriteLine("BuildFileDbContext is null");
                    return GetFallbackStationList();
                }

                // Check if CoStations is null
                if (_buildFileContext.CoStations == null)
                {
                    System.Diagnostics.Debug.WriteLine("CoStations DbSet is null");
                    return GetFallbackStationList();
                }

                // Try to execute a simple count query first to test the connection
                try
                {
                    int count = await _buildFileContext.CoStations.CountAsync();
                    System.Diagnostics.Debug.WriteLine($"Found {count} station records in database");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to count stations: {ex.Message}");
                }

                // Now try to get the actual data
                var areas = await _buildFileContext.CoStations
                    .Where(s => !string.IsNullOrEmpty(s.Station) && s.Status == "Active") // Add Status check if applicable
                    .Select(s => s.Station)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToListAsync();

                // Log the retrieved data
                System.Diagnostics.Debug.WriteLine($"Retrieved {areas?.Count ?? 0} areas from database");

                // If no areas found, use fallback list
                if (areas == null || !areas.Any())
                {
                    System.Diagnostics.Debug.WriteLine("No areas found in database, using fallback");
                    return GetFallbackStationList();
                }

                return areas;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllStationAreas: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return GetFallbackStationList();
            }
        }

        public List<string> GetFallbackStationList()
        {
            var fallbackList = new List<string>
            {
                "Emergency Department",
                "ICU",
                "Medical Ward",
                "Surgical Ward",
                "Pediatrics",
                "Maternity",
                "Cardiology",
                "Oncology",
                "Neurology",
                "Orthopedics"
            };
            System.Diagnostics.Debug.WriteLine($"Returning {fallbackList.Count} fallback stations");
            return fallbackList;
        }
    }
}
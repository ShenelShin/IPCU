using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;

public class StationController : Controller
{
    private readonly IConfiguration _configuration;

    public StationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /* public async Task<IActionResult> Index()
     {
         var stations = new List<Dictionary<string, object>>();

         string connectionString = _configuration.GetConnectionString("Build_FileConnection");

         using (SqlConnection conn = new SqlConnection(connectionString))
         {
             await conn.OpenAsync();
             string query = "SELECT TOP 1000 * FROM tbCoStation";
             SqlCommand cmd = new SqlCommand(query, conn);
             SqlDataReader reader = await cmd.ExecuteReaderAsync();

             while (await reader.ReadAsync())
             {
                 var row = new Dictionary<string, object>();
                 for (int i = 0; i < reader.FieldCount; i++)
                 {
                     row[reader.GetName(i)] = reader.GetValue(i);
                 }
                 stations.Add(row);
             }
         }

         ViewBag.Data = stations;
         return View();
     }*/




    public async Task<IActionResult> Index()
    {
        var stations = new List<Dictionary<string, object>>();
        int occupiedCount = 0;
        int notOccupiedCount = 0;
        int totalRooms = 0;

        string connectionString = _configuration.GetConnectionString("BuildFileConnection");

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();

            // Fetch station list
            string stationQuery = "SELECT TOP 1000 * FROM tbCoStation";
            SqlCommand stationCmd = new SqlCommand(stationQuery, conn);
            SqlDataReader stationReader = await stationCmd.ExecuteReaderAsync();

            while (await stationReader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < stationReader.FieldCount; i++)
                {
                    row[stationReader.GetName(i)] = stationReader.GetValue(i);
                }
                stations.Add(row);
            }
            stationReader.Close();

            // Fetch Occupied room count
            string occupiedQuery = @"
            SELECT COUNT(*) 
            FROM tbCoRoom r
            WHERE EXISTS (
                SELECT 1 
                FROM Patient_Data..tbpatient p
                WHERE p.RoomID = r.RoomID AND p.DcrDate IS NULL
            )";
            SqlCommand occupiedCmd = new SqlCommand(occupiedQuery, conn);
            occupiedCount = (int)await occupiedCmd.ExecuteScalarAsync();

            // Fetch Not Occupied room count
            string notOccupiedQuery = @"
            SELECT COUNT(*) 
            FROM tbCoRoom r
            WHERE NOT EXISTS (
                SELECT 1 
                FROM Patient_Data..tbpatient p
                WHERE p.RoomID = r.RoomID AND p.DcrDate IS NULL
            )";
            SqlCommand notOccupiedCmd = new SqlCommand(notOccupiedQuery, conn);
            notOccupiedCount = (int)await notOccupiedCmd.ExecuteScalarAsync();

            totalRooms = occupiedCount + notOccupiedCount;
        }

        ViewBag.Data = stations;
        ViewBag.OccupiedCount = occupiedCount;
        ViewBag.NotOccupiedCount = notOccupiedCount;
        ViewBag.TotalRooms = totalRooms;
        return View();
    }

    public async Task<IActionResult> ViewRooms(string stationId, string station)
    {
        var rooms = new List<Dictionary<string, object>>();
        string connectionString = _configuration.GetConnectionString("BuildFileConnection");

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();

            string query = @"
                            SELECT r.*,
                                   CASE 
                                       WHEN EXISTS (
                                           SELECT 1 
                                           FROM Patient_Data..tbpatient p
                                           WHERE p.RoomID = r.RoomID AND p.DcrDate IS NULL
                                       )
                                       THEN 'Occupied'
                                       ELSE 'Not Occupied'
                                   END AS OccupancyStatus
                            FROM tbCoRoom r
                            WHERE r.StationID = @StationID";


            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StationID", stationId);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                rooms.Add(row);
            }
        }

        ViewBag.StationID = stationId;
        ViewBag.Rooms = rooms;
        ViewBag.Station = station;
        return View();
    }



}

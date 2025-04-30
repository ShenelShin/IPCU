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

    public async Task<IActionResult> Index()
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
    }
    public async Task<IActionResult> ViewRooms(string stationId, string station)
    {
        var rooms = new List<Dictionary<string, object>>();

        // Get the connection string for Build_File from the configuration
        string connectionString = _configuration.GetConnectionString("Build_FileConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'BuildFile' is missing or invalid.");
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            // Updated query to handle StationID as varchar
            string query = "SELECT * FROM tbCoRoom WHERE StationID = @StationID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StationID", stationId); // stationId is now a string
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

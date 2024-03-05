using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Projekt.Models;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostTemperature([FromBody] TemperatureDataModel temperatureData)
        {
            string connectionString = "Data Source=temperaturdb.database.windows.net;Initial Catalog=temperaturDB;User ID=tempadmin;Password=HejHopp!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string sqlString = "INSERT INTO Temp (Temperature) VALUES (@Temperature)";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    using (SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection))
                    {
                        dbCommand.Parameters.AddWithValue("@Temperature", temperatureData.Temperature);

                        dbCommand.ExecuteNonQuery();
                    }
                }
                return Ok(); // Successfully inserted the data
            }
            catch (Exception ex)
            {
                // Log the error message here instead of returning it
                // Consider using a logging framework
                return StatusCode(500, "Internal Server Error: Could not insert temperature data");
            }
        }
    }

    public class TemperatureDataModel
    {
        public float Temperature { get; set; }
        public DateTime MeasurementTime { get; set; }
    }
}

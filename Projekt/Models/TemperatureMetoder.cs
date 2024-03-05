using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;


namespace Projekt.Models
 
{
    
    public class TemperatureMetoder
    {
        private string connectionString = "Data Source=temperaturdb.database.windows.net;Initial Catalog=temperaturDB;User ID=tempadmin;Password=HejHopp!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public List<TemperatureDetaljer> GetTemperatures(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection(connectionString);
            string sqlString = "SELECT TOP 10 * FROM Temp ORDER BY MeasurementTime DESC";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            SqlDataReader reader = null;
            List<TemperatureDetaljer> temperatureList = new List<TemperatureDetaljer>();

            errormsg = "";

            {
                try
                {
                    dbConnection.Open();
                    reader = dbCommand.ExecuteReader();

                    {
                        while(reader.Read())
                        {
                            TemperatureDetaljer temperature = new TemperatureDetaljer();
                            temperature.TempID = Convert.ToInt32(reader["TempID"]);
                            temperature.Temperature = Convert.ToSingle(reader["Temperature"]);
                            temperature.MeasurementTime = reader["MeasurementTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["MeasurementTime"]);
                            temperatureList.Add(temperature);
                        }
                        reader.Close();
                        return temperatureList;
                    }
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                    return new List<TemperatureDetaljer>();
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
        public TemperatureDetaljer GetLatestTemperature(out string errorMessage)
        {
            TemperatureDetaljer latestTemperature = null;
            errorMessage = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    string sqlString = "SELECT TOP 1 * FROM Temp ORDER BY MeasurementTime DESC";
                    SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

                    dbConnection.Open();
                    using (SqlDataReader reader = dbCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            latestTemperature = new TemperatureDetaljer
                            {
                                TempID = Convert.ToInt32(reader["TempID"]),
                                Temperature = Convert.ToSingle(reader["Temperature"]),
                                MeasurementTime = reader["MeasurementTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["MeasurementTime"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return latestTemperature;
        }

    }
}

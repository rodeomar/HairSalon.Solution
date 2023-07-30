using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using MySqlConnector;

public class StylistController : Controller
{
    // Helper method to get the database connection
    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(DBConfiguration.ConnectionString);
    }

}

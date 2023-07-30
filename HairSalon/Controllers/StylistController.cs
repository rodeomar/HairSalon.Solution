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
    // Action to view a list of all stylists
    public IActionResult Index()
    {
        List<Stylist> stylists = new List<Stylist>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Stylist";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Stylist stylist = new Stylist
                    {
                        StylistId = reader.GetInt32("StylistId"),
                        Name = reader.GetString("Name"),
                        Specialties = reader.GetString("Specialties")
                    };
                    stylists.Add(stylist);
                }
            }
        }
        return View(stylists);
    }

}

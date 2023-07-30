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

    // Action to search for stylists by name
    public IActionResult Search(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            // If the search query is empty or null, redirect to the Index action
            return RedirectToAction("Index");
        }

        List<Stylist> searchResults = GetStylistsByName(name);
        return View("Index", searchResults);
    }

    // Helper method to get stylists by name from the database
    private List<Stylist> GetStylistsByName(string name)
    {
        List<Stylist> stylists = new List<Stylist>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Stylist WHERE Name LIKE @Name";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", $"%{name}%");
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
        return stylists;
    }


    // Action to view details of a specific stylist
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Stylist stylist = GetStylistById(id.Value);
        if (stylist == null)
        {
            return NotFound();
        }

        return View(stylist);
    }

    // Helper method to get a stylist by ID from the database
    private Stylist GetStylistById(int id)
    {
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Stylist WHERE StylistId = @StylistId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StylistId", id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Stylist
                    {
                        StylistId = reader.GetInt32("StylistId"),
                        Name = reader.GetString("Name"),
                        Specialties = reader.GetString("Specialties")
                    };
                }
            }
        }
        return null;
    }
}

using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using MySqlConnector;

public class StylistController : Controller
{
    private static MySqlConnection GetConnection()
    {
        return new MySqlConnection(DBConfiguration.ConnectionString);
    }
    public IActionResult Index()
    {
        
        return View(GetStylistsByName(""));
    }

    public IActionResult Search(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return RedirectToAction("Index");
        }

        List<Stylist> searchResults = GetStylistsByName(name);
        return View("Index", searchResults);
    }

    public static List<Stylist> GetStylistsByName(string name)
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

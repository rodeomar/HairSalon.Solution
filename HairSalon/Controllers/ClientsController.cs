using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using MySqlConnector;

public class ClientsController : Controller
{
    private static MySqlConnection GetConnection()
    {
        return new MySqlConnection(DBConfiguration.ConnectionString);
    }

    public IActionResult Index()
    {
        ViewBag.Stylists = StylistController.GetStylistsByName("");
        List<Client> clients = new List<Client>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Client";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Client client = new Client
                    {
                        ClientId = reader.GetInt32("ClientId"),
                        Name = reader.GetString("Name"),
                        StylistId = reader.GetInt32("StylistId")
                    };
                    clients.Add(client);
                }
            }
        }
        return View(clients);
    }

    [HttpPost]
    public IActionResult Create(string name, int? stylistId)
    {
        if (ModelState.IsValid)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Client (Name, StylistId) VALUES (@Name, @StylistId)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@StylistId", stylistId ?? 0); 
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
        return View("Add");
    }
}

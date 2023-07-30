using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using MySqlConnector;
using System.Xml.Linq;

public class ClientsController : Controller
{
    private static MySqlConnection GetConnection()
    {
        return new MySqlConnection(DBConfiguration.ConnectionString);
    }



    private List<Client> GetAllClientsByName(string? Name)
    {
        List<Client> clients = new List<Client>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Client WHERE Name Like @Name;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", $"%{Name}%");
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
        return clients;
    }

    private List<Client> GetAllClientsByStylistId(int? StylistId)
    {
        List<Client> clients = new List<Client>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Client WHERE StylistId Like @StylistId;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StylistId", $"%{StylistId}%");
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
        return clients;
    }

 

    [HttpGet]
    public IActionResult Index(int? StylistId, string Name)
    {

        ViewBag.Stylists = StylistController.GetStylistsByName("");

        if (StylistId.HasValue)
        {
            Console.WriteLine(StylistId);
            List <Client> IDclients= GetAllClientsByStylistId(StylistId);
            Console.WriteLine(IDclients.Count);
            return View(IDclients);
        }


        Name = string.IsNullOrEmpty(Name)?"":Name;
        List<Client> clients = GetAllClientsByName(Name);
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
        return View();
    }
}

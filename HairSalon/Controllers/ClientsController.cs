using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(DBConfiguration.ConnectionString);
        }
        public IActionResult Index()
        {
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
    }
}

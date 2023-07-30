namespace HairSalon.Models
{
    public class Stylist
    {
        public int StylistId { get; set; }
        public string Name { get; set; }
        public string Specialties { get; set; }

        // Navigation property for the one-to-many relationship
        public List<Client> Clients { get; set; }
    }

}

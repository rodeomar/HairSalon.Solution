namespace HairSalon.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }

        // Foreign key for the one-to-many relationship
        public int StylistId { get; set; }
        public Stylist Stylist { get; set; }
    }

}

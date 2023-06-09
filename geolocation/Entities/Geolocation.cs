namespace geolocation.Entities
{
    public class Geolocation
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public int UserId { get; set; }
        public bool Visibility { get; set; }
        public DateTime Created { get; set; } 

    }
}
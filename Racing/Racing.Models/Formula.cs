namespace Racing.Models
{
    public class Formula
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Horsepower { get; set; }
        public int TopSpeed { get; set; }
        public double Acceleration { get; set; }
        public bool IsActive { get; set; }
        IList<Driver> Drivers { get; set; }
    }
}

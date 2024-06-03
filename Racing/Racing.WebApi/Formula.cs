namespace Racing.WebApi
{
    public class Formula
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Horsepower { get;  set; }
        public int TopSpeed { get; set; }
        public double Acceleration { get; set; }
        public bool IsActive { get; set; }
        IList<Driver> Drivers { get; set; }
        public Formula(Guid id, string name, int horsepower, int topSpeed, double acceleration, bool isActive)
        {
            Id = id;
            Name = name;
            Horsepower = horsepower;
            TopSpeed = topSpeed;
            Acceleration = acceleration;
            IsActive = isActive;
            Drivers = new List<Driver>();
        }

      
    }   
}

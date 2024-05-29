namespace Racing.WebApi
{
    public class Formula
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Horsepower { get;  set; }
        public int TopSpeed { get; set; }
        public double Acceleration { get; set; }
        public bool IsPraticipating { get; set; }
        public Driver Driver { get; set; }
    }
}

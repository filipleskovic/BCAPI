namespace Racing.WebApi.REST_Models
{
    public class FormulaGet
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Horsepower { get; set; }
        public double Acceleration { get; set; }
        public int TopSpeed { get; set; }
    }
}

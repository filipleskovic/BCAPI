namespace Racing.Models
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Guid FormulaId { get; set; }
        public Formula? Formula { get; set; }
        public bool IsActive { get; set; }

    }
}

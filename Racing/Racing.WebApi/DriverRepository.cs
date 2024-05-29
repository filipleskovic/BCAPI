namespace Racing.WebApi
{
    public static class DriverRepository
    {
        public static IList<Driver> drivers;

        static DriverRepository()
        {
            drivers = new List<Driver>();
        }
        public static void AddDriver(Driver driver)
        {
            int newID = drivers.Any() ? drivers.Max(d => d.ID) + 1 : 1;
            driver.ID = newID;
            drivers.Add(driver);
        }
        public static void DeleteDriver(Driver driver)
        {
            drivers.Remove(driver);
        }
        public static Driver GetDriverByiD(int id)
        {
            return drivers.FirstOrDefault(d => d.ID == id);
        }
        public static Driver UpdateDriver(Driver driver, Driver newDriver)
        {
            driver.FirstName = newDriver.FirstName;
            driver.LastName = newDriver.LastName;
            driver.Age = newDriver.Age;
            return driver;
        }
    }
}

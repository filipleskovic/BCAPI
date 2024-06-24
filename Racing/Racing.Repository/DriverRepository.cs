using Npgsql;
using Racing.Models;
namespace Racing.Repository
{
    public class DriverRepository:IRepository<Driver>
    {
        string connectionString;
        public DriverRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<Driver> GetAsync(Guid Id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            string cmdText = "SELECT * FROM \"Driver\" WHERE \"Id\"=@Id AND \"IsActive\"=@IsActive";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Id);
            command.Parameters.AddWithValue("@IsActive", true);
            _connection.Open();

            Driver driver = new Driver();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    driver.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    driver.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    driver.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    driver.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                    driver.FormulaId = reader.GetGuid(reader.GetOrdinal("FormulaId")); ;
                    driver.IsActive = true;

                }
            }
            _connection.Close();

            return driver;
        }
        public async Task<int> PostAsync(Driver newDriver)
        {

            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            string cmdText = "INSERT INTO \"Driver\"( \"Id\",\"FirstName\",\"LastName\",\"Age\",\"FormulaId\",\"IsActive\") VALUES(@Id,@FirstName,@LastName,@Age,@FormulaId,@IsActive)";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();

            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@FirstName", newDriver.FirstName);
            command.Parameters.AddWithValue("@LastName", newDriver.LastName);
            command.Parameters.AddWithValue("@Age", newDriver.Age);
            command.Parameters.AddWithValue("@FormulaId", newDriver.FormulaId);
            command.Parameters.AddWithValue("@IsActive", true);

            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return commits;
        }
        public async Task<int> PutAsync(Driver newDriver, Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            string cmdText = "UPDATE \"Driver\" SET \"FirstName\"=@FirstName, \"LastName\"=@LastName, \"Age\"=@Age, \"FormulaId\"=@FormulaId,\"IsActive\"=@IsActive WHERE \"Driver\".\"Id\"=@Id";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@FirstName", newDriver.FirstName);
            command.Parameters.AddWithValue("@LastName", newDriver.LastName);
            command.Parameters.AddWithValue("@Age", newDriver.Age);
            command.Parameters.AddWithValue("@FormulaId", newDriver.FormulaId);
            command.Parameters.AddWithValue("@IsActive", true);
            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return commits;
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            string cmdText = "UPDATE \"Driver\" SET \"IsActive\"=FALSE WHERE \"Driver\".\"Id\"=@Id";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("@Id", id);
            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return commits;

        }
        public async Task<IList<Driver>> GetAllAsync()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            IList<Driver> drivers = new List<Driver>();
            string cmdText = "SELECT * FROM \"Driver\" WHERE \"IsActive\"=@IsActive";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("IsActive", true);
            using (var reader = command.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    Driver driver = new Driver();
                    driver.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    driver.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    driver.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    driver.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                    driver.FormulaId = reader.GetGuid(reader.GetOrdinal("FormulaId")); ;
                    driver.IsActive = true;
                    drivers.Add(driver);
                }
            }
            _connection.Close();
            return drivers;
        }
    }
}
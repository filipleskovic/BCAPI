using Racing.Models.FormulaSearch;

using Npgsql;
using Racing.Models;
using System.Text;
namespace Racing.Repository
{
    public class FormulaRepository : IRepository<Formula>
    {
        public readonly string connectionString;
        public FormulaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<Formula> GetAsync(Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            string cmdText = "SELECT * FROM \"Formula\" WHERE \"Id\"=@Id AND \"IsActive\"=@IsActive";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);
            command.Parameters.AddWithValue("@IsActive", true);
            _connection.Open();

            Formula formula = new Formula();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    formula.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    formula.Name = reader.GetString(reader.GetOrdinal("Name"));
                    formula.Horsepower = reader.GetInt32(reader.GetOrdinal("Horsepower"));
                    formula.TopSpeed = reader.GetInt32(reader.GetOrdinal("TopSpeed"));
                    formula.Acceleration = reader.GetDouble(reader.GetOrdinal("Acceleration"));
                    formula.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                }
            }
            _connection.Close();
            return formula;
        }
        public async Task<int> PostAsync(Formula newFormula)
        {

            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            string cmdText = "INSERT INTO \"Formula\"( \"Id\",\"Name\",\"Horsepower\",\"TopSpeed\",\"Acceleration\",\"IsActive\") VALUES(@Id,@Name,@Horsepower,@TopSpeed,@Acceleration,@IsActive)";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();

            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@Name", newFormula.Name);
            command.Parameters.AddWithValue("@Horsepower", newFormula.Horsepower);
            command.Parameters.AddWithValue("@TopSpeed", newFormula.TopSpeed);
            command.Parameters.AddWithValue("@Acceleration", newFormula.Acceleration);
            command.Parameters.AddWithValue("@IsActive", true);

            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return commits;
        }
        public async Task<int> PutAsync(Formula newFormula, Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            string cmdText = "UPDATE \"Formula\" SET \"Name\"=@Name, \"Horsepower\"=@Horsepower, \"TopSpeed\"=@TopSpeed, \"Acceleration\"=@Acceleration,\"IsActive\"=@IsActive WHERE \"Formula\".\"Id\"=@Id";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", newFormula.Name);
            command.Parameters.AddWithValue("@Horsepower", newFormula.Horsepower);
            command.Parameters.AddWithValue("@TopSpeed", newFormula.TopSpeed);
            command.Parameters.AddWithValue("@Acceleration", newFormula.Acceleration);
            command.Parameters.AddWithValue("@IsActive", true);
            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return commits;
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            string cmdText = "UPDATE \"Formula\" SET \"IsActive\"=FALSE WHERE \"Formula\".\"Id\"=@Id";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("@Id", id);
            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return commits;

        }
        public async Task<IList<Formula>> GetAllAsync()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            IList<Formula> formulas = new List<Formula>();
            string cmdText = "SELECT * FROM \"Formula\" WHERE \"IsActive\"=@IsActive";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("IsActive", true);
            using (var reader = command.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    Formula formula = new Formula();
                    formula.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    formula.Name = reader.GetString(reader.GetOrdinal("name"));
                    formula.Horsepower = reader.GetInt32(reader.GetOrdinal("horsepower"));
                    formula.TopSpeed = reader.GetInt32(reader.GetOrdinal("topSpeed"));
                    formula.Acceleration = reader.GetDouble(reader.GetOrdinal("acceleration"));
                    formula.IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"));
                    formulas.Add(formula);
                }
            }
            _connection.Close();
            return formulas;
        }
        public async Task<IList<Formula>> GetAllAsync(FormulaGet formulaGet)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            IList<Formula> formulas = new List<Formula>();
            NpgsqlCommand command = new NpgsqlCommand("", _connection);
            command = MakeCommand(formulaGet,command);
            _connection.Open();
            using (var reader = command.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    Formula formula = new Formula();
                    formula.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    formula.Name = reader.GetString(reader.GetOrdinal("name"));
                    formula.Horsepower = reader.GetInt32(reader.GetOrdinal("horsepower"));
                    formula.TopSpeed = reader.GetInt32(reader.GetOrdinal("topSpeed"));
                    formula.Acceleration = reader.GetDouble(reader.GetOrdinal("acceleration"));
                    formula.IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"));
                    formulas.Add(formula);
                }
            }
            _connection.Close();
            return formulas;
        }

        private NpgsqlCommand MakeCommand(FormulaGet formulaGet,NpgsqlCommand command)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM \"Formula\" WHERE \"IsActive\" =@IsActive ");
            command.Parameters.AddWithValue("IsActive", true);

            if (formulaGet.filter.MaxTopSpeed == null && formulaGet.filter.MinTopSpeed != null)
            {
                builder.Append("AND \"TopSpeed\">@MinTopSpeed ");
                command.Parameters.AddWithValue("MinTopSpeed", formulaGet.filter.MinTopSpeed);
                
            }
            if (formulaGet.filter.MaxTopSpeed != null && formulaGet.filter.MinTopSpeed == null)
            {
                builder.Append("AND \"TopSpeed\"<@MaxTopSpeed ");
                command.Parameters.AddWithValue("MaxTopSpeed", formulaGet.filter.MaxTopSpeed);
            }
            if (formulaGet.filter.MaxTopSpeed != null && formulaGet.filter.MinTopSpeed != null)
            {
                builder.Append("AND \"TopSpeed\" BETWEEN @MinTopSpeed AND @MaxTopSpeed ");
                command.Parameters.AddWithValue("MaxTopSpeed", formulaGet.filter.MaxTopSpeed);
                command.Parameters.AddWithValue("MinTopSpeed", formulaGet.filter.MinTopSpeed);
            }
            if (formulaGet.filter.Name != null)
            {
                builder.Append("AND \"Name\" LIKE @Name");
                command.Parameters.AddWithValue("Name", formulaGet.filter.Name);
            }
            builder.Append($" ORDER BY \"{formulaGet.sort.OrderBy}\" {formulaGet.sort.OrderDirection}");
            command.CommandText= builder.ToString();
            return command;
        }
    }
}

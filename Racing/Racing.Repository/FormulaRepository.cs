using Racing.Models.FormulaSearch;
using Npgsql;
using Racing.Models;
using System.Text;
using Repository.Common;
namespace Racing.Repository
{
    public class FormulaRepository : IFormulaRepository
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
        public async Task<Formula> PostAsync(Formula newFormula)
        {

            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            string cmdText = "INSERT INTO \"Formula\"( \"Id\",\"Name\",\"Horsepower\",\"TopSpeed\",\"Acceleration\",\"IsActive\") VALUES(@Id,@Name,@Horsepower,@TopSpeed,@Acceleration,@IsActive)";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();

            command.Parameters.AddWithValue("@Id", newFormula.Id);
            command.Parameters.AddWithValue("@Name", newFormula.Name);
            command.Parameters.AddWithValue("@Horsepower", newFormula.Horsepower);
            command.Parameters.AddWithValue("@TopSpeed", newFormula.TopSpeed);
            command.Parameters.AddWithValue("@Acceleration", newFormula.Acceleration);
            command.Parameters.AddWithValue("@IsActive", newFormula.IsActive);

            int commits = await command.ExecuteNonQueryAsync();
            _connection.Close();
            return newFormula;
        }
        public async Task<int> PutAsync(Formula newFormula, Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            NpgsqlCommand command = new NpgsqlCommand("", _connection);
            command = MakeCommandUpdateFormula(newFormula, id, command);
            _connection.Open();

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
        public async Task<IList<Formula>> GetAllAsync(FormulaFilter filter, FormulaSort sort)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            IList<Formula> formulas = new List<Formula>();
            NpgsqlCommand command = new NpgsqlCommand("", _connection);
            command = MakeCommandGetAll(filter, sort, command);
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

        private NpgsqlCommand MakeCommandGetAll(FormulaFilter filter, FormulaSort sort, NpgsqlCommand command)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM \"Formula\" WHERE \"IsActive\" =@IsActive ");
            command.Parameters.AddWithValue("IsActive", true);

            if (filter.MaxTopSpeed == null && filter.MinTopSpeed != null)
            {
                builder.Append("AND \"TopSpeed\">@MinTopSpeed ");
                command.Parameters.AddWithValue("MinTopSpeed", filter.MinTopSpeed);

            }
            if (filter.MaxTopSpeed != null && filter.MinTopSpeed == null)
            {
                builder.Append("AND \"TopSpeed\"<@MaxTopSpeed ");
                command.Parameters.AddWithValue("MaxTopSpeed", filter.MaxTopSpeed);
            }
            if (filter.MaxTopSpeed != null && filter.MinTopSpeed != null)
            {
                builder.Append("AND \"TopSpeed\" BETWEEN @MinTopSpeed AND @MaxTopSpeed ");
                command.Parameters.AddWithValue("MaxTopSpeed", filter.MaxTopSpeed);
                command.Parameters.AddWithValue("MinTopSpeed", filter.MinTopSpeed);
            }
            if (filter.Name != null)
            {
                builder.Append("AND \"Name\" LIKE @Name");
                command.Parameters.AddWithValue("Name", filter.Name);
            }
            builder.Append($" ORDER BY \"{sort.OrderBy}\" {sort.OrderDirection}");
            command.CommandText = builder.ToString();
            return command;
        }
        private NpgsqlCommand MakeCommandUpdateFormula(Formula newFormula, Guid id, NpgsqlCommand command)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE \"Formula\" SET \"Id\" = @Id");
            command.Parameters.AddWithValue("@Id", id);
            if (newFormula.Horsepower != null)
            {
                builder.Append(" , \"Horsepower\"= @Horsepower");
                command.Parameters.AddWithValue("@Horsepower", newFormula.Horsepower);

            }
            if (newFormula.Acceleration != null)
            {
                builder.Append(" , \"Acceleration\"= @Acceleration");
                command.Parameters.AddWithValue("@Acceleration", newFormula.Acceleration);
            }
            if (newFormula.TopSpeed != null)
            {
                builder.Append(" , \"TopSpeed\"= @TopSpeed");
                command.Parameters.AddWithValue("@TopSpeed", newFormula.TopSpeed);
            }
            if (!string.IsNullOrEmpty(newFormula.Name))
            {
                builder.Append(" , \"Name\"= @Name");
                command.Parameters.AddWithValue("@Name", newFormula.Name);

            }
            builder.Append(" WHERE \"Id\"=   @Id");
            string cmdText = builder.ToString();
            command.CommandText = cmdText;
            return command;
        }

    }
}

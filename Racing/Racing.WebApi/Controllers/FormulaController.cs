using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Npgsql;
using System.Collections;
using System.Reflection.Metadata;
using System.Xml;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormulaController : ControllerBase
    {
        string connectionString = "\"Host=localhost;Port=5432;Database=Racing;User ID=postgres;Password=ficofika9;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=0\"";

        [HttpGet("GetFormula/{id:Guid}")]
        public IActionResult GetFormulaById(Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);
            string cmdText = "SELECT * FROM \"Formula\" WHERE \"Id\"=@Id AND \"IsActive\"=@IsActive";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@IsActive", true);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Guid Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    int horsepower = reader.GetInt32(reader.GetOrdinal("horsepower"));
                    int topSpeed = reader.GetInt32(reader.GetOrdinal("topSpeed"));
                    double acceleration = reader.GetDouble(reader.GetOrdinal("acceleration"));
                    bool isActive = reader.GetBoolean(reader.GetOrdinal("isActive"));

                    Formula formula = new Formula(Id, name, horsepower, topSpeed, acceleration, isActive);
                    _connection.Close();
                    return Ok(formula);
                }
                
            }
            return BadRequest();
        }

        [HttpPost("AddNewFormula")]
        public IActionResult AddFormula([FromBody] Formula newFormula)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            if (newFormula == null)
            {
                return BadRequest();
            }
            string cmdText = "INSERT INTO \"Formula\"( \"Id\",\"Name\",\"Horsepower\",\"TopSpeed\",\"Acceleration\",\"IsActive\") VALUES(@Id,@Name,@Horsepower,@TopSpeed,@Acceleration,@IsActive)";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();

            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@Name", newFormula.Name);
            command.Parameters.AddWithValue("@Horsepower", newFormula.Horsepower);
            command.Parameters.AddWithValue("@TopSpeed", newFormula.TopSpeed);
            command.Parameters.AddWithValue("@Acceleration", newFormula.Acceleration);
            command.Parameters.AddWithValue("@IsActive", true);
            var var = command.ExecuteNonQuery();
            _connection.Close();
            if (var>0)
            {
                return Ok(var);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteFormula/{id:Guid}")]
        public IActionResult DeleteFormulaById(Guid id)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            string cmdText = "UPDATE \"Formula\" SET \"IsActive\"=FALSE WHERE \"Formula\".\"Id\"=@Id";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("@Id", id);
            var var = command.ExecuteNonQuery();
            _connection.Close();
            return Ok();

        }
        [HttpPut("UpdateFormula/{id:Guid}")]
        public IActionResult UpdateDriverById(Guid id, [FromBody] Formula newFormula)
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
            var var = command.ExecuteNonQuery();
            _connection.Close();
            if (var > 0)
            {
                return Ok(var);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("Formulas")]
        public IActionResult GetDriverList()
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            IList<Formula> formulas = new List<Formula>();
            string cmdText = "SELECT * FROM \"Formula\" WHERE \"IsActive\"=@IsActive";
            NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
            _connection.Open();
            command.Parameters.AddWithValue("IsActive", true);
            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    Guid Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    int horsepower = reader.GetInt32(reader.GetOrdinal("horsepower"));
                    int topSpeed = reader.GetInt32(reader.GetOrdinal("topSpeed"));
                    double acceleration = reader.GetDouble(reader.GetOrdinal("acceleration"));
                    bool isActive = reader.GetBoolean(reader.GetOrdinal("isActive"));

                    Formula formula = new Formula(Id, name, horsepower, topSpeed, acceleration, isActive);
                    formulas.Add(formula);

                }
            }
            _connection.Close();
            return Ok(formulas);
        }
    }
}


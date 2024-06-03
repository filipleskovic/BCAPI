using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        string connectionString = "Host=localhost;Port=5432;Database=Racing;User ID=postgres;Password=ficofika9;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=0";

        [HttpGet("GetDriver/{id:Guid}")]
        public IActionResult GetDriverById(Guid id)
        {
            return Ok();
        }
        [HttpPost("AddNewDriver")]
        public IActionResult AddDriver([FromBody] Driver newDriver)
        {
            NpgsqlConnection _connection = new NpgsqlConnection(connectionString);

            {
                if (newDriver == null)
                {
                    return BadRequest();
                }
                string cmdText = "INSERT INTO \"Driver\"( \"Id\",\"FirstName\",\"LastName\",\"Age\",\"FormulaId\",\"IsActive\") VALUES(@Id,@FirstName,@LastName,@Age,@FormulaId,@IsActive)";
                NpgsqlCommand command = new NpgsqlCommand(cmdText, _connection);
                _connection.Open();

                command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@Name", newDriver.FirstName);
                command.Parameters.AddWithValue("@Horsepower", newDriver.LastName);
                command.Parameters.AddWithValue("@TopSpeed", newDriver.Age);
                command.Parameters.AddWithValue("@Acceleration", newDriver.FormulaId);
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
        }
        [HttpDelete("DeleteDriver/{id:Guid}")]
        public IActionResult DeleteDriverById(Guid id) {
            
            return Ok();

        }
        [HttpPut("UpdateDriver/{id:Guid}")]
        public IActionResult UpdateDriverById(Guid id, [FromBody] Driver newDriver)
        {
            
            return Ok(newDriver);
        }
        [HttpGet("Drivers")]
        public IActionResult GetDriverList()
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        [HttpGet("GetDriver/{id:int}")]
        public IActionResult GetDriverById(int id)
        {
            Driver driver = DriverRepository.GetDriverByiD(id);
            if (driver == null)
            {
                return NotFound(new { Message = "Driver not found." });
            }
            return Ok(driver);
        }
        [HttpPost("AddNewDriver")]
        public IActionResult AddDriver([FromBody] Driver newDriver)
        {
            if (newDriver == null)
            {
                return BadRequest();
            }
            DriverRepository.AddDriver(newDriver);
            return Ok(newDriver);
        }
        [HttpDelete("DeleteDriver/{id:int}")]
        public IActionResult DeleteDriverById(int id) {
            Driver driver = DriverRepository.GetDriverByiD(id);
            if (driver == null)
                return NotFound(new { Message = "Driver not found" });
            DriverRepository.drivers.Remove(driver);
            return Ok();

        }
        [HttpPut("UpdateDriver/{id:int}")]
        public IActionResult UpdateDriverById(int id, [FromBody] Driver newDriver)
        {
            if (newDriver == null)
            {
                return BadRequest();
            }
            Driver driver = DriverRepository.GetDriverByiD(id);
            if (driver == null)
                return NotFound(new { Message = "Driver not found" });
            driver=DriverRepository.UpdateDriver(driver, newDriver);
            return Ok(driver);
        }
        [HttpGet("Drivers")]
        public IActionResult GetDriverList()
        {
            return Ok(DriverRepository.drivers);
        }
    }
}

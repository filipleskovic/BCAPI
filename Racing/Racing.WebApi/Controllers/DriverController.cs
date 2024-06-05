
using Npgsql;
using Racing.Service;
using Racing.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        string connectionString = "Host=localhost;Port=5432;Database=Racing;User ID=postgres;Password=ficofika9;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=0";
        DriverService driverService;
        public DriverController()
        {
            driverService = new DriverService(connectionString);
        }
        [HttpGet("GetDriver/{id:Guid}")]
        public async Task<IActionResult> GetDriverById(Guid id)
        {
            try
            {
                Driver driver = await driverService.GetAsync(id);
                Debug.WriteLine(driver);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddNewDriver")]
        public async Task<IActionResult> AddDriver([FromBody] Driver newDriver)
        {
            if (newDriver == null)
            {
                return BadRequest();
            }
            try
            {
                int commits = await driverService.PostAsync(newDriver);

                if (commits == 0)
                {
                    return BadRequest();
                }
                return Ok($"Dodano vozaća: {commits}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteDriver/{id:Guid}")]
        public async Task<IActionResult> DeleteDriverById(Guid id) {

            try
            {
                int commits = await driverService.DeleteAsync(id);
                if (commits == 0)
                {
                    return BadRequest();
                }
                return Ok("Uspješno obrisana vozać");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateDriver/{id:Guid}")]
        public async Task<IActionResult> UpdateDriverById(Guid id, [FromBody] Driver newDriver)
        {

            try
            {
                int commits = await driverService.PutAsync(newDriver, id);
                if (commits == 0)
                {
                    return BadRequest();
                }
                return Ok("Uspješno updateana formula");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Drivers")]
        public async Task<IActionResult> GetDriverList()
        {
            try
            {
                IList<Driver> drivers = await driverService.GetAllAsync();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


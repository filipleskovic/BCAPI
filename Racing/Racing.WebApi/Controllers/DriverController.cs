
using Npgsql;
using Racing.Service;
using Racing.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Autofac.Core;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private IService<Driver> _Service;
        public DriverController(IService<Driver> _service)
        {
            _Service = _service;
        }
        [HttpGet("GetDriver/{id:Guid}")]
        public async Task<IActionResult> GetDriverById(Guid id)
        {
            try
            {
                Driver driver = await _Service.GetAsync(id);
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
                int commits = await _Service.PostAsync(newDriver);

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
                int commits = await _Service.DeleteAsync(id);
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
                int commits = await _Service.PutAsync(newDriver, id);
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
                IList<Driver> drivers = await _Service.GetAllAsync();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


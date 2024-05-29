using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormulaController: ControllerBase
    {
        [HttpGet("GetFormula/{id:int}")]
        public IActionResult GetFormulaById(int id)
        {
            Formula formula = FormulaRepository.GetFormulaByiD(id);
            if (formula == null)
            {
                return NotFound(new { Message = "Formula not found." });
            }
            return Ok(formula);
        }
        [HttpPost("AddNewFormula")]
        public IActionResult AddFormula([FromBody] Formula newFormula)
        {
            if (newFormula == null)
            {
                return BadRequest();
            }
            FormulaRepository.AddFormula(newFormula);
            DriverRepository.AddDriver(newFormula.Driver);
            return Ok(newFormula);
        }
        [HttpDelete("DeleteFormula/{id:int}")]
        public IActionResult DeleteFormulaById(int id)
        {
            Formula formula = FormulaRepository.GetFormulaByiD(id);
            if (formula == null)
                return NotFound(new { Message = "Formula not found" });
            FormulaRepository.DeleteFormula(formula);
            return Ok();

        }
        [HttpPut("UpdateFormula/{id:int}")]
        public IActionResult UpdateDriverById(int id, [FromBody] Formula newFormula)
        {
            if (newFormula == null)
            {
                return BadRequest();
            }
            Formula formula = FormulaRepository.GetFormulaByiD(id);
            if (formula == null)
                return NotFound(new { Message = "Formula not found" });
            formula = FormulaRepository.UpdateFormula(formula, newFormula);
            return Ok(formula);
        }
        [HttpGet("Formulas")]
        public IActionResult GetDriverList()
        {
            return Ok(FormulaRepository.formulas);
        }
    }
}

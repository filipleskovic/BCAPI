using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Npgsql;
using Racing.Models;
using Racing.Models.FormulaSearch;
using Racing.Service;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml;

namespace Racing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormulaController : ControllerBase
    {
        string connectionString = "Host=localhost;Port=5432;Database=Racing;User ID=postgres;Password=ficofika9;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=0";
        FormulaService formulaService;
        public FormulaController(IConfiguration configuration)
        {
            formulaService = new FormulaService(connectionString);
        }
        [HttpGet("GetFormula/{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Formula formula = await formulaService.GetAsync(id);
                Debug.WriteLine(formula);
                return Ok(formula);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddNewFormula")]
        public async Task<IActionResult> Post(Formula newFormula)
        {
            if (newFormula == null)
            {
                return BadRequest();
            }
            try
            {
                int commits = await formulaService.PostAsync(newFormula);

                if (commits == 0)
                {
                    return BadRequest();
                }
                return Ok($"Dodano formula: {commits}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteFormula/{id:Guid}")]
        public async Task<IActionResult> DeleteFormulaById(Guid id)
        {
            try
            {
                int commits = await formulaService.DeleteAsync(id);
                if (commits == 0)
                {
                    return BadRequest();
                }
                return Ok("Uspješno obrisana formula");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPut("UpdateFormula/{id:Guid}")]
        public async Task<IActionResult> UpdateFormulaById(Guid id, [FromBody] Formula newFormula)
        {
            try
            {
                int commits = await formulaService.PutAsync(newFormula, id);
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
        [HttpGet("Formulas")]
        public async Task<IActionResult> GetFormulaList()
        {
            try
            {
                IList<Formula> formulas = await formulaService.GetAllAsync();
                return Ok(formulas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("FilteredFormulas")]
        public async Task<IActionResult> Get(string? name=null, int? minTopSpeed=null, int? maxTopSpeed=null, string? orderDirection="ASC", string? orderBy = "Name")
        {
            try
            {
                FormulaGet formulaGet = new FormulaGet(new FormulaFilter(name, minTopSpeed, maxTopSpeed), new FormulaSort(orderBy, orderDirection));
                IList<Formula> formulas = await formulaService.GetAllAsync(formulaGet);
                return Ok(formulas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
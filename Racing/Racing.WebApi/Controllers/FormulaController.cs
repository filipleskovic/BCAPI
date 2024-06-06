using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Npgsql;
using Racing.Models;
using Racing.Models.FormulaSearch;
using Racing.Service;
using Service.Common;
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
        private IFormulaService _service;
        public FormulaController(IFormulaService _Service)
        {
            this._service = _Service;
        }
        [HttpGet("GetFormula/{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Formula formula = await _service.GetAsync(id);
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
                int commits = await _service.PostAsync(newFormula);

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
                int commits = await _service.DeleteAsync(id);
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
                int commits = await _service.PutAsync(newFormula, id);
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
                IList<Formula> formulas = await _service.GetAllAsync();
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
                IList<Formula> formulas = await _service.GetAllAsync(new FormulaFilter(name, minTopSpeed, maxTopSpeed), new FormulaSort(orderBy, orderDirection));
                return Ok(formulas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
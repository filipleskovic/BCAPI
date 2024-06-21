using Autofac.Core;
using Autofac.Features.Scanning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Npgsql;
using Racing.Models;
using Racing.Models.FormulaSearch;
using Racing.Service;
using Racing.WebApi.REST_Models;
using Racing.WebApi.RESTModels;
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
        private IMapper _mapper;
        public FormulaController(IFormulaService _service,IMapper _mapper)
        {
            this._service = _service;
            this._mapper = _mapper;
        }
        [HttpGet("GetFormula/{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Formula formula = await _service.GetAsync(id);
                return Ok(_mapper.Map<FormulaGet>(formula));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddNewFormula")]
        public async Task<IActionResult> Post(FormulaPost newFormula)
        {
            if (newFormula == null)
            {
                return BadRequest();
            }
            try
            {
                int commits = await _service.PostAsync(_mapper.Map<Formula>(newFormula));

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
        public async Task<IActionResult> UpdateFormulaById(Guid id, [FromBody] FormulaPut newFormula)
        {
            if (_service.GetAsync(id) == null)
                return NotFound("Ne postoji taj id u bazi");
            else
            {
                if (newFormula == null)
                {
                    return NotFound("Mora bit nesto");
                }
                try
                {
                    int commits = await _service.PutAsync(_mapper.Map<Formula>(newFormula), id);
                    if (commits == 0)
                    {
                        return NotFound("ss");
                    }
                    return Ok("Uspješno updateana formula");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
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
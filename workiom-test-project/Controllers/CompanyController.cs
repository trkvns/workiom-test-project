using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using workiom_test_project.Data.Interfaces;
using workiom_test_project.Models;

namespace workiom_test_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CompanyController : ControllerBase
    {
        private IDb Db { get; set; }

        private readonly ILogger<CompanyController> Logger;

        public CompanyController(IDb db, ILogger<CompanyController> logger)
        {
            Db = db;
            Logger = logger;
        }

        [HttpPost("add-column")]
        public async Task<IActionResult> AddColumn(NewColumn model)
        {
            try
            {
                if (await Db.Companies.AddColumnAsync(model))
                    return NoContent();

                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetCompany")]
        public async Task<IActionResult> GetCompany(string id)
        {
            try
            {
                Company item = await Db.Companies.GetByIdAsync(id);
                if (item != null)
                {
                    return Ok(item);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(Company model)
        {
            try
            {
                var item = await Db.Companies.CreateAsync(model);
                if (item != null)
                    return CreatedAtRoute("GetCompany", new { id = item.Id.ToString() }, item);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, Company model)
        {
            try
            {
                var item = Db.Companies.GetByIdAsync(id);

                if (item == null)
                    return NotFound();

                if (await Db.Companies.UpdateAsync(id, model))
                    return NoContent();

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var item = await Db.Companies.GetByIdAsync(id);

                if (item == null)
                    return NotFound();


                if (await Db.Companies.DeleteAsync(id))
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
          
        }
    }
}

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
    public class ContactController : ControllerBase
    {
        private IDb Db { get; set; }

        private readonly ILogger<ContactController> Logger;

        public ContactController(IDb db, ILogger<ContactController> logger)
        {
            Db = db;
            Logger = logger;
        }

        [HttpPost("add-column")]
        public async Task<IActionResult> AddColumn(NewColumn model)
        {
            try
            {
                if (await Db.Contacts.AddColumnAsync(model))
                    return NoContent();

                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(Dictionary<string, object> queries)
        {
            try
            {
                var items = await Db.Contacts.SearchAsync(queries);

                if (items != null)
                {
                    List<string> companyIds = new List<string>();
                    items.ForEach(c => companyIds.AddRange(c.CompanyIds));
                    var companies = await Db.Companies.GetByIdAsync(companyIds);

                    foreach (var item in items)
                        if (item.CompanyIds != null && item.CompanyIds.Count > 0)
                            item.Companies = companies.Where(c => item.CompanyIds.Contains(c.Id.ToString())).ToList();

                    return Ok(items);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetContact")]
        public async Task<IActionResult> GetContact(string id)
        {
            try
            {
                Contact item = await Db.Contacts.GetByIdAsync(id);

                if (item != null)
                {
                    if (item.CompanyIds != null && item.CompanyIds.Count > 0)
                        item.Companies = await Db.Companies.GetByIdAsync(item.CompanyIds);

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
        public async Task<IActionResult> CreateContact(Contact model)
        {
            try
            {
                var item = await Db.Contacts.CreateAsync(model);
                if (item != null)
                    return CreatedAtRoute("GetContact", new { id = item.Id.ToString() }, item);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, Contact model)
        {
            try
            {
                var item = await Db.Contacts.GetByIdAsync(id);

                if (item == null)
                    return NotFound();

                if (await Db.Contacts.UpdateAsync(id, model))
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
                var item = await Db.Contacts.GetByIdAsync(id);

                if (item == null)
                    return NotFound();


                if (await Db.Contacts.DeleteAsync(id))
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
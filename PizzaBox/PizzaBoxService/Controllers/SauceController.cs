using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Entities;

namespace PizzaBoxService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SauceController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public SauceController(CustomerRepo repo)
        {
            this.repo = repo;
        }
        // GET: api/<StoreController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Sauce> Get()
        {
            try
            {
                return Ok(repo.GetSauces());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Sauce> Get(int id)
        {
            try
            {
                return Ok(repo.getSauceById(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}

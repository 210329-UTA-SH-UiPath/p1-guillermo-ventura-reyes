using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Entities;

namespace PizzaBoxService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public ToppingController(CustomerRepo repo)
        {
            this.repo = repo;
        }
        // GET: api/<StoreController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Topping> Get()
        {
            try
            {
                return Ok(repo.GetToppings());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Topping> Get(int id)
        {
            try
            {
                return Ok(repo.getToppingById(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}

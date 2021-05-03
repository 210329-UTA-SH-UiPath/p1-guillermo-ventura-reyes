using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Entities;
using PizzaBox.Storing.Entities;
using System.Net.Mime;

namespace PizzaBoxService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public PizzaController(CustomerRepo repo)
        {
            this.repo = repo;
        }
        // GET: api/<StoreController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Pizza> Get()
        {
            try
            {
                return Ok(repo.GetPizzas());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            try
            {
                return Ok(repo.getPizzaById(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Consumes("application/json")]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<Pizza> Post(Pizza pizza)
        {
            if (pizza == null)
            {
                return BadRequest("The Customer you are trying to add is empty");
            }
            else
            {
                return repo.AddPizza(pizza);
            }

        }
    }
}

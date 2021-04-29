using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Entities;
using System.Net.Mime;


namespace PizzaBoxService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public OrderController(CustomerRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Consumes("application/json")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(Order order)
        {
            if (order == null)
            {
                return BadRequest("The Customer you are trying to add is empty");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    repo.addOrder(order);
                    return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
                }
            }

        }


        // GET: api/<StoreController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> Get()
        {
            try
            {
                return Ok(repo.getOrders());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            try
            {
                return Ok(repo.getOrderById(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}

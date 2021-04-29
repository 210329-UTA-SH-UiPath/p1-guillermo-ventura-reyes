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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public CustomerController(CustomerRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Consumes("application/json")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(Customer customer)
        {
            if (customer == null)
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
                    repo.AddCustomer(customer);
                    return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
                }
            }
            
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Customer> Put(Customer customer)
        {
            try
            {
                return Ok(repo.UpdateCustomer(customer));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        // GET: api/<StoreController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Customer> Get()
        {
            try
            {
                return Ok(repo.GetCustomers());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            try
            {
                return Ok(repo.getCustomerByID(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing.Repositories;
using PizzaBox.Storing.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaBoxService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public StoreController(CustomerRepo repo)
        {
            this.repo = repo;
        }
        // GET: api/<StoreController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Store> Get()
        {
            try
            {
                return Ok(repo.GetStores());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Store> Get(int id)
        {
            try
            {
                return Ok(repo.getStoreById(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        
    }
}

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
    public class CrustsController : ControllerBase
    {
        private readonly CustomerRepo repo;
        public CrustsController(CustomerRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Crust> Get()
        {
            try
            {
                return Ok(repo.GetCrusts());
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Crust> Get(int id)
        {
            try
            {
                return Ok(repo.getCrustById(id));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}

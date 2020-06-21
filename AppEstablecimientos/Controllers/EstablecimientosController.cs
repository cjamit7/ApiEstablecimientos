using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppEstablecimientos.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppEstablecimientos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstablecimientosController : ControllerBase
    {
        // GET: api/<EstablecimientosController>
        [HttpGet]
        public List<Establecimientos> Get()
        {
            return new Establecimientos().GetAll(); 
        }

        // GET api/<EstablecimientosController>/5
        [HttpGet("{id}")]
        public Establecimientos Get(int id)
        {
            return new Establecimientos().Get(id);
        }

        // POST api/<EstablecimientosController>
        [HttpPost]
        public ApiResponse Post([FromBody] Establecimientos obj)
        {
            return obj.Insert();
        }

        // PUT api/<EstablecimientosController>/5
        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] Establecimientos obj)
        {
            return obj.Update(id);
        }

        // DELETE api/<EstablecimientosController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new Establecimientos().Delete(id);
        }
    }
}

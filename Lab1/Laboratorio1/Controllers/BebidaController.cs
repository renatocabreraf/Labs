using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorio1.ArbolB;
using Laboratorio1.Modelos;

namespace Laboratorio1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BebidaController : ControllerBase
    {
        // GET: api/Bebida
        [HttpGet]
        public IEnumerable<Bebida> Get()
        {
            List<Bebida> myList = new List<Bebida>();
            myList = Datos.Instance.myTree.getList();
            myList.Sort(Datos.CompararBebida);
            return myList;
        }

        // GET: api/Bebida/5
        [HttpGet("{id}", Name = "Get")]
        public Bebida Get(string id)
        {
            return Datos.Instance.myTree.getElement(id);
        }

        // POST: api/Bebida
        [HttpPost]
        public void Post([FromBody] Bebida value)
        {
            Datos.Instance.myTree.Add(value);
        }

        // PUT: api/Bebida/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
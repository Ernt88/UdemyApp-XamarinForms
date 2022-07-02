using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdemyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        // GET: api/<CursoController>
        [HttpGet]
        public ApiResponse Get()
        {
            return new CursoModel().GetAll();
        }

        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            return new CursoModel().Get(id);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] CursoModel model)
        {
            return new CursoModel().Add(model);
        }

        [HttpPut]
        public ApiResponse Put([FromBody] CursoModel model)
        {
            return new CursoModel().Update(model);
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new CursoModel().Delete(id);
        }

        //// GET api/<CursoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CursoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CursoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CursoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

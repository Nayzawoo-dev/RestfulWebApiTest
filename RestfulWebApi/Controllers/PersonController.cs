using DataBaseConnectionSharedLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using RestfulWebApiTest.Models;
using RestfulWebApiTest.Services;

namespace RestfulWebApi.Controllers
{
    // https://localhost:3000/api/Person // this is domain and Person is endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
       private readonly IPersonServices _personServices;

        public PersonController(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpGet]
        public IActionResult GetPerson([FromServices] IPersonServices person)
        {
            var data = person.GetPerson();
            return Ok(data);
        }

        [HttpGet("Detail/{id}")]
        //[HttpGet("{password}/{username}")]
        public IActionResult GetDetailPerson(int id)
        {
           
            var model = _personServices.GetPersonById(id);
            if (model.Success == false)
            {
                return BadRequest(model);
            };

            return Ok(model);
            
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonModels person)
        {
            Console.WriteLine(person.ToJson1());
            var model = _personServices.PostPerson(person);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult CreateOrUpdatePerson([FromBody] PersonModels person, int id)
        {
            var model = _personServices.UpdateAndPostPerson(id,person);
            if (!model.Success)
            {
                return BadRequest(model);
            }
            return Ok(model);

        }




        [HttpPatch("{id}")]
        public IActionResult UpdatePerson([FromBody] PersonModels person, int id)
        {
            var res = _personServices.GetPersonById(id);
            if(res.Success == false)
            {
                return NotFound(res);
            }
            var model = _personServices.UpdatePerson(id, person);
            if (model.Success == false)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }



        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var res = _personServices.GetPersonById(id);
            if (res.Success == false)
            {
                return NotFound(res);
            }
            var model = _personServices.DeletePerson(id);
            if(model.Success == false)
            {
                return BadRequest(model);
            }
           return Ok(model);
        }
    }
}

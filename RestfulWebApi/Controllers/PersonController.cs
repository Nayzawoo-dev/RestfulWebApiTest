using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestfulWebApi.Controllers
{
    // https://localhost:3000/api/Person // this is domain and Person is endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPerson()
        {
            return Ok("GetPerson");
        }

        [HttpGet("Detail")]
        public IActionResult GetDetailPerson()
        {
            return Ok("One Person Detail");
        }

        [HttpPost]
        public IActionResult CreatePerson()
        {
            return Ok("PostPerson");
        }

        [HttpPut]
        public IActionResult CreateOrUpdatePerson()
        {
            return Ok("Create or Update Person");
        }

        [HttpPatch]
        public IActionResult UpdatePerson()
        {
            return Ok("Update Person");
        }

        [HttpDelete]
        public IActionResult DeletePerson()
        {
            return Ok("Delete Person");
        }
    }
}

using DataBaseConnectionSharedLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestfulWebApiTest.Models;

namespace RestfulWebApi.Controllers
{
    // https://localhost:3000/api/Person // this is domain and Person is endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DapperServices _dapperservices;
        public PersonController()
        {
            _dapperservices = new DapperServices(new SqlConnectionStringBuilder
            {
                DataSource = "DELL",
                InitialCatalog = "DotNetTraining",
                UserID = "SA",
                Password = "root",
                TrustServerCertificate = true,
            });
        }
        [HttpGet]
        public IActionResult GetPerson()
        {
            var list = _dapperservices.Query<PersonModels>("select * from Tbl_Window");
            var data = new
            {
                Success = true,
                Information = list,
                Message = "Successful"
            };
            return Ok(data);
        }

        [HttpGet("Detail")]
        [HttpGet("{password}")]
        public IActionResult GetDetailPerson(string password)
        {
            _dapperservices.Query<PersonModels>("select * from Tbl_Window where Password = @Password",new )
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

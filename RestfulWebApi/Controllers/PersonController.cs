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

        [HttpGet("Detail/{password}")]
        //[HttpGet("{password}/{username}")]
        public IActionResult GetDetailPerson(string password)
        {
           var list = _dapperservices.Query<PersonModels>
                ("select * from Tbl_Window where Password = @Password", new PersonModels
            {
                Password = password,
            });
            if(list.Count == 0)
            {
                var data = new { 
                Success = false,
                Message = "Person Not Found!"
                };

                return NotFound(data);
            }

            var data2 = new {
            Success = true,
            Information = list[0]
            };

            return Ok(data2);
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonModels person)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Window]
           ([UserName]
           ,[Password])
     VALUES
           (@UserName
           ,@Password)";
            int res = _dapperservices.Execute(query, person);
            var data = new
            {
                Success = res > 0,
                Message = res > 0 ? "Complete" : "Fail"
            };
            return Ok(data);
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

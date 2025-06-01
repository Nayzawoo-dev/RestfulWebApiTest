using System;
using DataBaseConnectionSharedLibrary;
using Microsoft.Data.SqlClient;
using RestfulWebApiTest.Models;

namespace RestfulWebApiTest.Services
{
    //Business Logic Layer And Data Access Layer
    public class PersonServices
    {
        private readonly DapperServices _dapperServices;

        public PersonServices()
        {
            _dapperServices = new DapperServices(new SqlConnectionStringBuilder
            {
                DataSource = "DELL",
                InitialCatalog = "DotNetTraining",
                UserID = "SA",
                Password = "root",
                TrustServerCertificate = true
            });
        }

        public ResponseModels GetPerson()
        {
            var list = _dapperServices.Query<PersonModels>("select * from Tbl_Window");
            var data = new ResponseModels
            {
                Success = true,
                Information = list,
                Message = "Successful"
            };
            return (data);
        }

        public ResponseModels GetPersonById(int id)
        {
            var list = _dapperServices.Query<PersonModels>
                 ("select * from Tbl_Window where Id = @Id", new PersonModels { Id = id });

            if (list.Count == 0)
            {
                var model = new ResponseModels
                {
                    Success = false,
                    Message = "Person Not Found!"
                };

                return model;
            }

            var models = new ResponseModels
            {
                Success = true,
                Information = list[0]
            };

            return models;
        }

        public ResponseModels PostPerson(PersonModels requestmodel)
        {
            if (requestmodel.UserName.IsNullOrEmpty1())
            {
                return new ResponseModels
                {
                    Success = false,
                    Message = "Username is required"
                };
            }
            Console.WriteLine(requestmodel.ToJson());
            string query = @"INSERT INTO [dbo].[Tbl_Window]
           ([UserName]
           ,[Password])
     VALUES
           (@UserName
           ,@Password)";
            int res = _dapperServices.Execute(query,requestmodel);
            var model = new ResponseModels
            {
                Success = res > 0,
                Message = res > 0 ? "Complete" : "Fail"
            };
            return model;
        }

        public ResponseModels UpdateAndPostPerson(int id,PersonModels requestmodel)
        {
            requestmodel.Id = id;
            var list = _dapperServices.Query<PersonModels>("select * from Tbl_Window where Id=@Id",requestmodel);
            if (list.Count == 0)
            {
                string query = @"INSERT INTO [dbo].[Tbl_Window]
           ([UserName]
           ,[Password])
     VALUES
           (@UserName
           ,@Password)";

                var res = _dapperServices.Execute(query, requestmodel);
                var model = new ResponseModels
                {
                    Success = res > 0,
                    Message = res > 0 ? "Your Id is Invalid So, We are insert" : "Fail"
                };
                return model;
            }
            else
            {
                string field = string.Empty;

                if (requestmodel.UserName != null && !string.IsNullOrEmpty(requestmodel.UserName.Trim()))
                {
                    field += "[UserName] = @UserName,";
                }

                if (requestmodel.Password != null && !string.IsNullOrEmpty(requestmodel.Password.Trim()))
                {
                    field += "Password = @Password,";
                }
                if (field.Length == 0)
                {
                    return (new ResponseModels
                    {
                        Message = "No Field To Update"
                    });
                }

                if (field.Length > 0)
                {
                    field = field.Substring(0, field.Length - 1);
                }

                string query = $@"UPDATE [dbo].[Tbl_Window]
       SET {field}
       WHERE Id=@Id";
                
                var res2 = _dapperServices.Execute(query, requestmodel);
                var model = new ResponseModels
                {
                    Success = res2 > 0,
                    Message = res2 > 0 ? "Success" : "Fail"

                };
                return model;
            }
            ;
        }

        public ResponseModels UpdatePerson(int id,PersonModels requestmodel)
        {
            string field = string.Empty;
            requestmodel.Id = id;

            if (requestmodel.UserName != null && !string.IsNullOrEmpty(requestmodel.UserName.Trim()))
            {
                field += "[UserName] = @UserName,";
            }

            if (requestmodel.Password != null && !string.IsNullOrEmpty(requestmodel.Password.Trim()))
            {
                field += "[Password] = @Password,";
            }

            if (field.Length == 0)
            {
                var model = new ResponseModels
                {
                    Success = false,
                    Message = "No Field To Update"
                };
                return model;
            }

            if (field.Length > 0)
            {
                field = field.Substring(0, field.Length - 1);
            }

            string query = $@"UPDATE [dbo].[Tbl_Window]
         SET 
         {field}
 WHERE Id=@Id";


            int res = _dapperServices.Execute(query, requestmodel);

            var models = new ResponseModels
            {
                Success = res > 0,
                Message = res > 0 ? "Successful" : "Fail"
            };
            return models;

        }

        public ResponseModels DeletePerson(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Window]
      WHERE Id= @Id";
            int res = _dapperServices.Execute(query, new PersonModels
            {
                Id = id
            });

            var model = new ResponseModels
            {
                Success = res > 0,
                Message = res > 0 ? "Delete Successful" : "Delete Fail"

            };

            return model;
        }



    }
}

using RestfulWebApiTest.Models;

namespace RestfulWebApiTest.Services
{
    public interface IPersonServices
    {
        ResponseModels DeletePerson(int id);
        ResponseModels GetPerson();
        ResponseModels GetPersonById(int id);
        ResponseModels PostPerson(PersonModels requestmodel);
        ResponseModels UpdateAndPostPerson(int id, PersonModels requestmodel);
        ResponseModels UpdatePerson(int id, PersonModels requestmodel);
    }
}
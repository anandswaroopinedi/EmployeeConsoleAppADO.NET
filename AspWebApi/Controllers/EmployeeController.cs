using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IDataOperations _dataOperations;
        public EmployeeController(IDataOperations dataOperations) 
        {
            _dataOperations = dataOperations;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Employee>>Get()
        {
            return await _dataOperations.GetEmployees();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Employee> Get(string id)
        {
            return _dataOperations.GetEmployeeById(id).Result;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post(string id,string FirstName,string LastName, string DateOfBirth, string Email, string MobileNumber, string JoinDate, string JobTitle, string Location, string Department, string ManagerId, string Project)
        {
            Employee employee=new Employee();
            employee.Id = id;
            employee.FirstName=FirstName;
            employee.LastName=LastName;
            employee.DateOfBirth=DateOfBirth;
            employee.Email=Email;
            employee.MobileNumber=MobileNumber;
            employee.JoinDate=JoinDate;
            employee.JobTitle=JobTitle;
            employee.Location=Location;
            employee.Department=Department;
            employee.ManagerId=ManagerId;
            employee.Project=Project;
            _dataOperations.AddEmployeeToDb(employee);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _dataOperations.DeleteEmployee(id);
        }
    }
}

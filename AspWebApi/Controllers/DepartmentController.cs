using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDataOperations _dataOperations;
        public DepartmentController(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            return await _dataOperations.GetDepartments();
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public void Post([FromBody] string name)
        {
            Department department=new Department();
            department.Name = name;
            _dataOperations.AddDepartmentToDb(department);
        }
    }
}

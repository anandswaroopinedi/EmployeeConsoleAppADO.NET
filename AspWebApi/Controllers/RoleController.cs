using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IDataOperations _dataOperations;
        public RoleController(IDataOperations dataOperations) 
        {
            _dataOperations = dataOperations;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public async Task< IEnumerable<Roles>> Get()
        {
            return await _dataOperations.GetRoles();
        }
        [HttpPost]
        public void Post(string rolename,string department, string Location,string Description)
        {
            Roles roles=new Roles();
            roles.Name = rolename;
            roles.Department = department;
            roles.Location = Location;
            roles.Description = Description;
            _dataOperations.AddRoleToDb(roles);
        }

    }
}

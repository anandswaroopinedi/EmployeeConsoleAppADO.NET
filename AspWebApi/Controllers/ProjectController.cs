using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IDataOperations _dataOperations;
        public ProjectController(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }
        // GET: api/<ProjectController>
        [HttpGet]
        public async  Task<IEnumerable<Project>> Get()
        {
            return await _dataOperations.GetProjects();
        }


        // POST api/<ProjectController>
        [HttpPost]
        public void Post([FromBody] string name)
        {
            Project project=new Project();
            project.Name = name;
            _dataOperations.AddProjectToDb(project);
        }
    }
}

using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IDataOperations _dataOperations;
        public LocationController(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }

        // GET: api/<LocationController>
        [HttpGet]
        public async Task<IEnumerable<Location>> Get()
        {
            return await _dataOperations.GetLocations();
        }

        // POST api/<LocationController>
        [HttpPost]
        public void Post([FromBody] string name)
        {
            Location location = new Location();
            location.Name = name;
            _dataOperations.AddLocationToDb(location);
        }
    }
}

using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.AspNetCore.Mvc;

namespace Dotel2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationSearchController : ControllerBase
    {
        private readonly DotelDBContext _dbContext;
        private IRentalRepository rentalRepository;

        public LocationSearchController(DotelDBContext dbContext, IRentalRepository repository)
        {
            _dbContext = dbContext;
            rentalRepository = repository;
        }

        [HttpGet]
        public IActionResult Get(string query) {
            if(string.IsNullOrEmpty(query))
            {
                return Ok(new List<String>());
            }
            var suggestions= rentalRepository.getSuggestLocation(query);
            if(suggestions!=null)
            {
                return Ok(suggestions);
            }
            return Ok(new List<string>());
        }

    }
}

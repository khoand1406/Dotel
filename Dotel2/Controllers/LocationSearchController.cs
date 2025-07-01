using Dotel2.Models;
using Dotel2.Repository.Rental;
using Dotel2.Service.Rental;
using Microsoft.AspNetCore.Mvc;

namespace Dotel2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationSearchController : ControllerBase
    {
        private IRentalService _rentalService;

        public LocationSearchController(IRentalService service)
        {
            _rentalService = service;
        }

        [HttpGet]
        public IActionResult Get(string query) {
            if(string.IsNullOrEmpty(query))
            {
                return Ok(new List<String>());
            }
            var suggestions= _rentalService.getSuggestLocation(query);
            if(suggestions!=null)
            {
                return Ok(suggestions);
            }
            return Ok(new List<string>());
        }

    }
}

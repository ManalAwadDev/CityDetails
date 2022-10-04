using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityDetails.Models;
using CityDetails.ExternalResources;
using CityDetails.IntermediateModels.Incoming;
using Microsoft.AspNetCore.Authorization;

namespace CityDetails.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityInformationContext _context;
         
        public CitiesController(CityInformationContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.City.ToListAsync();
        }


        [HttpGet("{cityName}")]
        public async Task<ActionResult<City[]>> GetCityByName(string cityName)
        {
            var cities = await _context.City.Where(city => city.Name.Contains(cityName)).ToArrayAsync();

            if (cities.Equals(null))
            {
                return NotFound();
            }

            foreach (var city in cities)
            {
                var country = await new ExternalApiClient().GetCountryDetails(city.Country);
                var weather = await new ExternalApiClient().GetWeatherDetails(city.Name);
                if (country != null)
                {

                }
                if (weather != null)
                {

                }
            }

            return cities;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, CityDelta cityDelta)
        {
            if (id != cityDelta.Id)
            {
                return BadRequest();
            }
            var city = await _context.City.FindAsync(id);

            city.TouristRating = (bool)(cityDelta?.TouristRating.HasValue) ? cityDelta?.TouristRating : city.TouristRating;
            city.EstablishedDate = (bool)(cityDelta?.EstablishedDate.HasValue) ? cityDelta?.EstablishedDate : city.EstablishedDate;
            city.EstimatedPopulation = (bool)(cityDelta?.EstimatedPopulation.HasValue) ? cityDelta?.EstimatedPopulation : city.EstimatedPopulation;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.City.Add(city);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return city;
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}

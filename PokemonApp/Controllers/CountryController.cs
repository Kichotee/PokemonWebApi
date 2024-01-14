using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]

        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]

        public IActionResult GetCountry(int Id)
        {
            if (!_countryRepository.CountryExists(Id))
            {
                return NotFound();
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(Id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(country);
        }
        [HttpGet("country/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]

        public IActionResult GetCountryByOwner(int ownerId)
        {
            if (!_countryRepository.CountryExists(ownerId))
            {
                return NotFound();
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(country);
        }


        [HttpGet("owners/{countryId}")]
        [ProducesResponseType(200, Type = typeof(List<Owner>))]
        [ProducesResponseType(400)]

        public IActionResult GetOwnersByCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }
            var owners = _countryRepository.GetOwnersbyCountry(countryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(owners);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry ([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var country = _countryRepository.GetCountries().Where(c => c.Name
            .TrimEnd().ToUpper() ==
            countryCreate
            .Name.Trim().ToUpper()
            ).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (country != null)
            {
                ModelState.AddModelError("", "Country already Exists");
                return StatusCode(422, ModelState);

            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Someting went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Country created successfully");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]


        public IActionResult UpdateCategory(int countryId, [FromBody] CountryDto countryUpdate)
        {
            if (countryUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != countryUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryMap = _mapper.Map<Country>(countryUpdate);

            if (!_countryRepository.UpdateCountry(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updateing state");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

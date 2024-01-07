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
    }
}

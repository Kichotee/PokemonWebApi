//using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using System.Collections;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;


        }

        //private StatusCodeResult  OwnerRepositoryExists(int Id)
        //{
        //    if (!_ownerRepository.OwnerExists(Id))
        //    {
        //        return NotFound();
        //    }
            
        //}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {


            List<OwnerDto> owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owners);

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Owner))]
        public IActionResult GetOwner(int Id)
        {

            if (!_ownerRepository.OwnerExists(Id))
            {
                return NotFound();
            }

            OwnerDto owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(Id));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);

        }

        [HttpGet("{Id}/pokemon")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetOwnersOfPokemon(int Id)
        {

            if (!_ownerRepository.OwnerExists(Id))
            {
                return NotFound();
            }

            List<OwnerDto> pokemons = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnerOfAPokemon(Id));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);

        }
        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(List<PokemonDto>))]
        public IActionResult GetPokemonsOfOwner(int pokemonId)
        {

            if (!_ownerRepository.OwnerExists(pokemonId))
            {
                return NotFound();
            }

            List<PokemonDto> owners = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonsOfOwner(pokemonId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);

        }

    }
}

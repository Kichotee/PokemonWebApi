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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateOwner([FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
            {
                return BadRequest(ModelState); 
                
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var owner = _ownerRepository.GetOwners().Where(
                c => c.LastName.ToUpper().Trim() == ownerCreate.LastName.ToUpper().Trim())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already Exists");
                return StatusCode(422, ModelState);
            }
            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Someting went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Category created succesfully");

        }

    }
}


using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]


    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;


        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]


        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemon(int pokeId)
        {

            if (!_pokemonRepository.GetPokemonExists(pokeId))
            {
                return NotFound();
            }
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.GetPokemonExists(pokeId))
            {
                return NotFound();
            }
            var rating = _pokemonRepository.GetPokemonRating(pokeId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(rating);


        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId,
            [FromQuery] int categoryId,
            [FromBody] PokemonDto pokemon)

        {

            if (pokemon == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonRepository.GetPokemons().Where(p => p.Name.
            Trim().
            ToLower() == pokemon.Name.Trim().ToLower()).FirstOrDefault();

            if (pokemons!=null)
            {
                ModelState.AddModelError("", "Pokemon Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var pokemonMap = _mapper.Map<Pokemon>(pokemon);

            if (!_pokemonRepository.CreatePokemon(ownerId,categoryId,pokemonMap))
            {
                ModelState.AddModelError("", "Error While saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Created Successfully");
        }

    }
}

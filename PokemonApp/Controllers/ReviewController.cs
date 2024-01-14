using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]

    public class ReviewController :Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository , IMapper mapper)
        {

            _reviewRepository = reviewRepository;
            _mapper = mapper;   

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]

        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(reviews);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]

        public IActionResult GetReview(int Id)
        {
            if (!_reviewRepository.ReviewExists(Id))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(Id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewsOfPokemon(int pokemonId)
        {
            if (!_reviewRepository.ReviewExists(pokemonId))
            {
                return NotFound();
            }
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsofAPokemon(pokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(reviews);
        }

        
    }
}

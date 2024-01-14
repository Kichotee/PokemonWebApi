using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using System.Collections;

namespace PokemonApp.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(reviewers);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewer(int Id)
        {
            if (!_reviewerRepository.ReviewerExists(Id))
            {
                return NotFound();
            }

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(Id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(reviewer);
        }

        [HttpGet("{Id}/Reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetReviews(int Id)

        {
            if (!_reviewerRepository.ReviewerExists(Id))
            {
                return NotFound();
            }

            var reviewers = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(Id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            return Ok(reviewers);
        }
    }
}

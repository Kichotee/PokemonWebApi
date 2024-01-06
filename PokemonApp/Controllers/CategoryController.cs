using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;


        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]


        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List>
        }
    }
}

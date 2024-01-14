using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    //[DisableCors]
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
            var categories = _mapper.Map<List<CategoryDto>>
                (_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(categories);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int Id)
        {
            if (!_categoryRepository.CategoryExists(Id))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(Id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]

        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var category = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokeMonByCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
            {

                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryRepository.GetCategories().Where(c => c.Name
                .Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already Exists");
                return StatusCode(422, ModelState);
            }



            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Someting went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Category created succesfully");

        }
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]


        public IActionResult UpdateCategory(int categoryId ,[FromBody] CategoryDto categoryUpdate)
        {
            if (categoryUpdate==null)
            {
                return BadRequest(ModelState);
            }

            if(categoryId!=categoryUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryMap = _mapper.Map<Category>(categoryUpdate);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updateing state");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

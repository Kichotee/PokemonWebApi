
using AutoMapper;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class ReviewRepository:IReviewRepository
    {

        
        private readonly DataContext _context;
        public ReviewRepository( DataContext context) { 
        
      
            _context = context;
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsofAPokemon(int id)
        {
            return _context.Reviews.Where(o => o.Pokemon.Id ==id).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(o => o.Pokemon.Id ==id);

        }
    }
}

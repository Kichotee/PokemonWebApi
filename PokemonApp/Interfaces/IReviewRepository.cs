using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();   

        Review GetReview(int id);   

        ICollection<Review> GetReviewsofAPokemon(int id);    

        bool ReviewExists(int id);
    }
}

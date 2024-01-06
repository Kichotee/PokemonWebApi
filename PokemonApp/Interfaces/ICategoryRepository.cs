using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection <Category> GetCategories();

        Category GetCategory (int id);

        ICollection<Pokemon> GetPokeMonByCategory(int id);

        bool CategoryExists(int id);
    }
}

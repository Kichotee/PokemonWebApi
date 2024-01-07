using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int Id);

        ICollection<Owner> GetOwnerOfAPokemon(int pokeId);

        ICollection<Pokemon> GetPokemonsOfOwner(int ownerId);

        bool OwnerExists(int Id);
    }
}

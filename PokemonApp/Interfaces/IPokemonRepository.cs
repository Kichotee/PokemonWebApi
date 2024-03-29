﻿using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        
        decimal GetPokemonRating(int pokeId);

        bool GetPokemonExists(int pokeId);

        bool CreatePokemon(int categoryId, int ownerId, Pokemon pokemon );

        bool Save();

    }
}

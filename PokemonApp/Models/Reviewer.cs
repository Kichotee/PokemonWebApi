﻿namespace PokemonApp.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Review> Reviews { get; set; }

        //public ICollection<Pokemon> Pokemon { get; set; }

    }
}

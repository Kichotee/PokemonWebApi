﻿using AutoMapper;
using PokemonApp.Dto;
using PokemonApp.Models;

namespace PokemonApp.Helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();

            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();


        }
    }
}

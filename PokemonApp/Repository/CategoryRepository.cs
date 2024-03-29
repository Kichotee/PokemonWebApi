﻿using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {

            _context = context;

        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(p => p.Id == id);
        }

       

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokeMonByCategory(int id)
        {
            return _context.PokemonCategories
                  .Where(pc => pc.CategoryId == id)
                  .Select(c => c.Pokemon)
                  .ToList();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);

            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }

}

using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;


        public CountryRepository(DataContext context)
        {

            _context = context;

        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(p => p.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(p => p.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(p => p.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners
                .Where(po => po.Id == ownerId)
                .Select(p => p.Country)
                .FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersbyCountry(int countryId)
        {
            return _context.PokemonOwners
                .Where(po => po.OwnerId == countryId)
                .Select(p => p.Owner)
                .ToList();
        }
    }
}
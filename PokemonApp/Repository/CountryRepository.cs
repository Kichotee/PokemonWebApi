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

       

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
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
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(p => p.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);

            return Save();
        }
        public bool UpdateCountry(Country country)
        {
            _context.Update(country); return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

       
    }
}
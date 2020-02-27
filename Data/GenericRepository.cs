using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace DatingApp.API.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users
                .Include(i => i.Photos)
                .Include(c => c.Country)
                .Include(s => s.State)
                .Include(ct => ct.City)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Include(p => p.Photos)
                .Include(c => c.City)
                .Include(s => s.State)
                .Include(c => c.Country)
                .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.Take(50).ToListAsync();    
        }

        public async Task<IEnumerable<State>> GetStates()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.Where(w => w.Id == id).FirstOrDefaultAsync();
        }
    }
}

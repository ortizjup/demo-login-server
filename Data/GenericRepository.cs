using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
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

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users
               .Where(w => w.Id != userParams.UserId && w.Gender == userParams.Gender)
               .Include(p => p.Photos)
               .Include(c => c.City)
               .Include(s => s.State)
               .Include(c => c.Country).OrderByDescending(o => o.LastActived).AsQueryable();

            //if the user specify something diferent to default values 
            //here we need to calculate the years based on the number provided
            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Now.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Now.AddYears(-userParams.MinAge);

                users = users.Where(w => w.DateOfBirth >= minDob && w.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(o => o.Created);
                        break;
                    default:
                        users = users.OrderByDescending(o => o.LastActived);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
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

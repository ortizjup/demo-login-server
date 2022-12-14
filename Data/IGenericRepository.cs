using DatingApp.API.Helpers;
using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public interface IGenericRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<PagedList<User>> GetUsers(UserParams userParams);

        Task<User> GetUser(int id);

        Task<IEnumerable<State>> GetStates();

        Task<IEnumerable<Country>> GetCountries();

        Task<IEnumerable<City>> GetCities();

        Task<Photo> GetPhoto(int id);
    }
}

using DatingApp.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data.SeedJsons
{
    public class Seed
    {
        public static void SeedUSers(DataContext context)
        {
            try
            {
                if (!context.Users.Any())
                {
                    var userData = System.IO.File.ReadAllText("Data/SeedDb/UserSeedData.json");

                    var users = JsonConvert.DeserializeObject<List<User>>(userData);

                    foreach (var user in users)
                    {
                        byte[] passWordHash, passWordSalt;

                        CreatePasswordHash("password", out passWordHash, out passWordSalt);

                        user.PasswordHash = passWordHash;
                        user.PasswrodSalt = passWordSalt;
                    }

                    context.AddRange(users);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SeedCities(DataContext context)
        {
            try
            {
                if (!context.Cities.Any())
                {
                    var citiesData = System.IO.File.ReadAllText("Data/SeedDb/CitiesDataSeed.json");

                    var cities = JsonConvert.DeserializeObject<List<City>>(citiesData);

                    context.AddRange(cities);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SeedCountries(DataContext context)
        {
            try
            {
                if (!context.Countries.Any())
                {
                    var countriesdata = System.IO.File.ReadAllText("Data/SeedDb/CountriesDataSeed.json");

                    var countries = JsonConvert.DeserializeObject<List<Country>>(countriesdata);

                    context.AddRange(countries);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SeedStates(DataContext context)
        {
            try
            {
                if (!context.States.Any())
                {
                    var StateData = System.IO.File.ReadAllText("Data/SeedDb/StatesDataSeed.json");

                    var states = JsonConvert.DeserializeObject<List<State>>(StateData);

                    context.AddRange(states);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };
        }
    }
}

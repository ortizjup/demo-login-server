using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Acces-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Acces-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime DateOfBirth)
        {
            var age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_Mvc_DependencyInjection.Models;

namespace WebApps_Mvc_DependencyInjection.Services
{
    public class ProfileOptionsService
    {
        public List<string> ListGenders()
        {
            // keeping this simple
            return new List<string>() { "Female", "Male" };
        }

        public List<State> ListStates()
        {
            // a few states from USA
            return new List<State>()
            {
                new State("Alabama", "AL"),
                new State("Alaska", "AK"),
                new State("Ohio", "OH")
            };
        }

        public List<string> ListColors()
        {
            return new List<string>() { "Blue", "Green", "Red", "Yellow" };
        }
    }
}

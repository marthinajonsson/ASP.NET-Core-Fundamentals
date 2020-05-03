using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant Update(Restaurant updatedRestaurant);
        int Commit();
        Restaurant GetById(int? restaurantId);
        Restaurant Add(Restaurant restaurant);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>() { 
                new Restaurant { Id=1, Name="Apa", Location="Narnia", Cuisine = CuisineType.Italian },
                new Restaurant { Id=2, Name="Nisse", Location="Pärleporten", Cuisine = CuisineType.Mexican },
                new Restaurant { Id=3, Name="Ivar", Location="Barkarby", Cuisine = CuisineType.Indian },
                new Restaurant { Id=4, Name="Adam", Location="Barkarby", Cuisine = CuisineType.Indian }
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant GetById(int? id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restuarant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restuarant != null)
            {
                restuarant.Name = updatedRestaurant.Name;
                restuarant.Location = updatedRestaurant.Location;
                restuarant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restuarant;
        }
    }
}


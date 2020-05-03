using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data;
using OdeToFood.Core;

namespace OdeToFood.Pages.Restaurant
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration Config;
        private readonly IRestaurantData restaurantData;

        public string Message { get; set; }
        
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Core.Restaurant> Restaurants { get; set; }
        public ListModel (IConfiguration config, IRestaurantData restaurantData)
        {
            this.Config = config;
            this.restaurantData = restaurantData;
        }
        public void OnGet()
        {
            Message = Config["Message"];   
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}

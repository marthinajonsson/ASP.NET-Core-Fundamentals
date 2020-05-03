using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using RestaurantModel = OdeToFood.Core.Restaurant;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

      
        [BindProperty]
        public RestaurantModel Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId);
            }
            else
            {
                Restaurant = new RestaurantModel();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page(); // this one will loose all changes if the user refreshes the browser
            }

            if(Restaurant.Id > 0)
            {
                Restaurant = restaurantData.Update(Restaurant);
            }else
            {
                restaurantData.Add(Restaurant);
            }
            
            restaurantData.Commit();
            TempData["Message"] = "Restaurant Saved!"; // will be cleaned automatically
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id }); // this fix the refresh issue
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        [Required, StringLength(80)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Location { get; set; }
        public int Id { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}

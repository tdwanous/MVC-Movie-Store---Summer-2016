using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class HomeModel
    {
        public Movie FeaturedMovie { get; set; }
        public Movie DiscountMovie { get; set; }
        public Movie UpcomingMovie { get; set; }

    }
}
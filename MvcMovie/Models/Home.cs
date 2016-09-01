using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Home
    {
        public Movie FeaturedMovie { get; set; }
        public Movie DiscountedMovie { get; set; }
        public Movie UpcomingMovie { get; set; }
        public string FeaturedMovieTitle { get; set; }
        public string DiscountedMovieTitle { get; set; }
        public string UpcomingMovieTitle { get; set;}
        public decimal FeaturedPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal DiscountDiscount { get; set; }
        public decimal UpcomingPrice { get; set; }
    }
}
using MvcMovie.Models;
using MvcMovie.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public ActionResult Index()
        {
            var HomeModel = new Home();
            HomeModel.FeaturedMovie = _movieService.fMovie();
            HomeModel.FeaturedMovieTitle = HomeModel.FeaturedMovie.Title;
            HomeModel.FeaturedPrice = HomeModel.FeaturedMovie.Price;
            HomeModel.DiscountedMovie = _movieService.dMovie();
            HomeModel.DiscountedMovieTitle = HomeModel.DiscountedMovie.Title;
            HomeModel.DiscountPrice = HomeModel.DiscountedMovie.Price;
            HomeModel.DiscountDiscount = Convert.ToDecimal(HomeModel.DiscountedMovie.DiscountPrice);
            HomeModel.UpcomingMovie = _movieService.uMovie();
            HomeModel.UpcomingMovieTitle = HomeModel.UpcomingMovie.Title;
            HomeModel.UpcomingPrice = HomeModel.UpcomingMovie.Price;
            return View(HomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "IS 445 Project in Collaboration with Wolters Kluwer Financial Services";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
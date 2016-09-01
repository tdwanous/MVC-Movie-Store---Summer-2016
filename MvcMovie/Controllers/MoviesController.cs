using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using MvcMovie.Models.Identity;
using MvcMovie.Services;
using PagedList;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: /Movies/
        public ActionResult Index(string movieGenre, string searchString, string currentFilter, string sortOrder, int? page) { 
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var GenreLst = new List<string>();

            var movies = _movieService.GetAllMovies();
            var GenreQry = from d in movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            if (searchString != null)
            {
                page = 1;
            }else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            switch(sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "Date":
                    movies = movies.OrderBy(m => m.ReleaseDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseDate);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break; 
            }

            int pageSize = 10;
            
            int pageNumber = (page ?? 1);
            return View(movies.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Movie movie = _movieService.GetMovieById(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
           return View(movie);
        }

        // GET: /Movies/Create
        public ActionResult Create()
        {
            if(User.IsInRole("Admin"))
            {
                return View(new Movie
                {
                    Genre = "Comedy",
                    Price = 3.99M,
                    ReleaseDate = DateTime.Now,
                    Rating = "G",
                    Title = "Ghost Busters III"
                });
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: /Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating,posterLink")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.CreateMovie(movie);
                return RedirectToAction("Index", "Admin");
            }

            return View(movie);
        }

        // GET: /Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if(User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Movie movie = _movieService.GetMovieById(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
                return View(movie);
            }
            return RedirectToAction("Index", "Admin");
        }

        // POST: /Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating,posterLink,DiscountPrice,isFeatured,isDiscounted")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.EditMovie(movie);
                return RedirectToAction("Index", "Admin");
            }
            return View(movie);
        }

        // GET: /Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if(User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Movie movie = _movieService.GetMovieById(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
                return View(movie);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _movieService.DeleteMovie(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}

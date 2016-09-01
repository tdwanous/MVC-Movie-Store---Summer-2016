using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMovie.Models;
using MvcMovie.Models.Identity;
using System.Data.Entity;

namespace MvcMovie.Services
{
    public class MovieService : IMovieService
    {
        static Random rnd = new Random();
        private readonly ApplicationDbContext _dbContext;

        public MovieService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Movie CreateMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public void DeleteMovie(int id)
        {
            var movieToRemove = _dbContext.Movies.Find(id);
            if (movieToRemove != null)
            {
                _dbContext.Movies.Remove(movieToRemove);
            }
            _dbContext.SaveChanges();
        }

        public Movie EditMovie(Movie movie)
        {
            _dbContext.Entry(movie).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return movie;
        }

        public Movie GetMovieById(int? id)
        {
            return _dbContext.Movies.Find(id);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _dbContext.Movies.AsEnumerable();
        }

        public Movie fMovie()
        {
            var fMovies = _dbContext.Movies.AsEnumerable().Where(i => i.isFeatured == true).ToList();
            int randomNumber = rnd.Next(fMovies.Count());
            var fMovie = fMovies[randomNumber];
            return fMovie;
        }

        public Movie dMovie()
        {
            var dMovies = _dbContext.Movies.AsEnumerable().Where(i => i.isDiscounted == true).ToList();
            int randomNumber = rnd.Next(dMovies.Count());
            var dMovie = dMovies[randomNumber];
            return dMovie;
        }

        public Movie uMovie()
        {
            var uMovies = _dbContext.Movies.AsEnumerable().Where(i => i.ReleaseDate > DateTime.Now).ToList();
            int randomNumber = rnd.Next(uMovies.Count());
            var uMovie = uMovies[randomNumber];
            return uMovie;
        }
    }
}
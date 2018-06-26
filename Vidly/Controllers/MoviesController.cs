using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //[Route("movies/release/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }

            return Content(string.Format("pageIndex={0}&sortby={1}", pageIndex, sortBy));
        }

        [Route("Movies")]
        public ActionResult Movies()
        {
            var viewModel = new MovieViewModel()
            {
                Movies = _context.Movies.ToList()
            };
            return View(viewModel);
        }

        [Authorize(Roles = "CanManageMovies")]
        [Route("Movies/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var viewModel = new MovieViewModel();
            var movies = _context.Movies;
            viewModel.GenreTypes = _context.GenreTypes.ToList();

            viewModel.Movie = movies.SingleOrDefault(c => c.Id == id);
            if (viewModel.Movie == null)
            {
                //return HttpNotFound();
                viewModel.Movie = new Movie() { Name = "Movie not found." };
            }
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles ="CanManageMovies")]
        [Route("Movies/New")]
        public ActionResult New()
        {
            var viewModel = new MovieViewModel();
            var movies = _context.Movies;
            viewModel.GenreTypes = _context.GenreTypes.ToList();

            viewModel.Movie = new Movie();
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = "CanManageMovies")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                // add new movie
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                // edit existing movie
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Movies");
        }
    }
}
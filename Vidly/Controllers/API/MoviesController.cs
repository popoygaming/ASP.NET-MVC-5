using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var retValue = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(retValue);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var newMovie = Mapper.Map<MovieDto, Movie>(MovieDto);
            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            MovieDto.Id = newMovie.Id;
            return Created(new Uri(Request.RequestUri.ToString()), MovieDto);
        }

        // PUT /api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (MovieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(MovieDto, MovieInDb);

            _context.SaveChanges();

            return Ok(MovieInDb);
        }

        // DELETE /api/Movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (MovieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(MovieInDb);
            _context.SaveChanges();

            return Ok(Mapper.Map<Movie, MovieDto>(MovieInDb));
        }
    }
}

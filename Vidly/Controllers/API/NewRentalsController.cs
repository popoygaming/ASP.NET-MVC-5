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
    public class NewRentalsController : ApiController
    {
        ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult NewRentals(NewRentalDto newRental)
        {
            if (ModelState.IsValid)
            {
                if (newRental.MovieIds.Count == 0)
                    return BadRequest("No movies provided.");

                var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);

                // defensive approach
                if (customer == null)
                    return BadRequest("Customer ID not found");

                var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

                if (movies.Count != newRental.MovieIds.Count)
                    return BadRequest("One or more movies are not valid.");

                foreach (var movie in movies)
                {
                    if (movie.NumberAvailable <= 0)
                        return BadRequest(movie.Name + " is not available.");

                    movie.NumberAvailable--;
                    var rents = new Rental()
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now,
                        DateReturned = null
                    };
                    _context.Rentals.Add(rents);
                }
                _context.SaveChanges();
                return Ok(movies);
            }
            return BadRequest("Invalid Rental Data");
        }
    }
}

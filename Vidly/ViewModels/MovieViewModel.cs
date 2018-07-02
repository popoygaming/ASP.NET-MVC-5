using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieViewModel
    {
        public List<Movie> Movies { get; set; }

        public Movie Movie { get; set; }

        public string FormActionName
        {
            get
            {
                return Movie == null || Movie.Id == 0 ? "New Movie" : "Edit Movie";
            }
        }

        public List<Genre> Genres { get; set; }
    }
}
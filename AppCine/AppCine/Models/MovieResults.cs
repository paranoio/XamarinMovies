using System;
using System.Collections.Generic;
using System.Text;

namespace AppCine.Models
{
    class MovieResults
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public IList<Movie> results { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AppCine.Models
{
    public class Cast
    {
        public int cast_id { get; set; }
        public string character { get; set; }
        public string credit_id { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public string profile_path { get; set; }

        public string baseImageUrl { get; set; }
        public string Thumbnail
        {
            get
            {
                return baseImageUrl + profile_path;
            }
        }
    }

    public class Crew
    {
        public string credit_id { get; set; }
        public string department { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string job { get; set; }
        public string name { get; set; }
        public string profile_path { get; set; }
    }

    public class MovieCredits
    {
        public int id { get; set; }
        public IList<Cast> cast { get; set; }
        public IList<Crew> crew { get; set; }
    }
}

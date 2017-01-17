using System;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        public string Production { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public DateTime? PremiereDate { get; set; }
        public int Length { get; set; }
        public int NumberOfShows { get; set; }
    }
}

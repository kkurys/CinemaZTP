using System;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

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
        public string ShortDate
        {
            get
            {
                return PremiereDate == null ? "" : PremiereDate.Value.ToString("dd/MM/yyyy");
            }
        }
        public BitmapImage Image
        {
            get
            {
                if (ImageFileName != null)
                {
                    return new BitmapImage(new Uri(ImageFileName));
                }
                else
                {
                    return new BitmapImage(new Uri("Content\no-icon.png", UriKind.Relative));
                }
            }
        }
        public override string ToString()
        {
            return Title;
        }

    }
}

using System;
using System.Collections.ObjectModel;

namespace Cinema.Models
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime? ShowDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Hall { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ObservableCollection<Reservation> Reservation { get; set; }
        public string ShortDate
        {
            get
            {
                return ShowDate.Value.ToString("dd-MM-yyyy");
            }
        }
        public string ShortStartTime
        {
            get
            {
                return StartTime.ToString(@"hh\:mm");
            }
        }
        public string ShortEndTime
        {
            get
            {
                return EndTime.ToString(@"hh\:mm");
            }
        }
        public string Title
        {
            get
            {
                return Movie.Title;
            }
        }
    }
}

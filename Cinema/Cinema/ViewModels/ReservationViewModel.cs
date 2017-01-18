using Cinema.Interfaces;
using System;

namespace Cinema.ViewModels
{
    public class ReservationViewModel : IReservationViewModel, IObserver
    {

        // OBSERVER METHODE
        #region observer
        public void Update(Type t)
        {
      //      _reservation = new ObservableCollection<Reservation>(_db.GetObjects<Reservation>());
        }
        #endregion
    }
}

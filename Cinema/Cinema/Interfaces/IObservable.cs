using System;

namespace Cinema.Interfaces
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void NotifyObservers(Type t);
    }
}

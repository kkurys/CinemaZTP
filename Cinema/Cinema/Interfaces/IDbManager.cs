using System.Collections.Generic;

namespace Cinema.Interfaces
{
    public interface IDbManager : IObservable
    {
        IDbManager GetInstance();
        ICollection<T> GetObjects<T>();
        void Add(object obj);
        void Update(object obj);
        void Delete(object obj);
    }
}

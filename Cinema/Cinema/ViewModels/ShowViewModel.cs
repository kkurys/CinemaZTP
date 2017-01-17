using Cinema.Interfaces;
using System;

namespace Cinema.ViewModels
{
    public class ShowViewModel : IShowViewModel
    {
        public void Filter(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Init(IDbManager db)
        {
            throw new NotImplementedException();
        }
    }
}

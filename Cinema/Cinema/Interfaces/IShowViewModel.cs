namespace Cinema.Interfaces
{
    interface IShowViewModel : IBaseViewModel
    {
        void Filter(params object[] parameters);

    }
}

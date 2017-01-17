using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Cinema.Interfaces
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public abstract void Init(IDbManager db);
        public ListCollectionView GetView<T>(ObservableCollection<T> container)
        {
            return (ListCollectionView)CollectionViewSource.GetDefaultView(container);
        }
    }
}

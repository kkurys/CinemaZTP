using System.Windows.Input;

namespace Cinema
{
    public static class WpfCommands
    {
        public static readonly RoutedUICommand Add = new RoutedUICommand("Add", "Add", typeof(WpfCommands));
    }
}

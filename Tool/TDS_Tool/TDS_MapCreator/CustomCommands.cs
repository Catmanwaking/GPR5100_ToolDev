//Author: Dominik Dohmeier
using System.Windows.Input;

namespace TDS_MapCreator
{
    internal static class CustomCommands
    {
        public static RoutedUICommand ShowGridLines = new RoutedUICommand
        (
            "Show Gridlines",
            "ShowGridLines",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.G, ModifierKeys.Control)
            }
        );
    }
}
//Author: Dominik Dohmeier
using System.Windows;
using System.Windows.Input;
using TDS_MapCreator.Tiles;

namespace TDS_MapCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MapManager MapManager { get; private set; }
        public static TileSeletionManager TileSeletionManager { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            TileSeletionManager = new TileSeletionManager(TileTypeSelector, TileSelector, ColorSelector);
            MapManager = new MapManager(MapGrid, MapWidth, MapHeight);
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => MapManager.CreateNewMap();

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => MapManager.LoadMap();

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => MapManager.SaveMap();

        private void Quit_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private void ShowGridlinesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MapGrid.ShowGridLines ^= true;
            gridLines.IsChecked = MapGrid.ShowGridLines;
        }

        private void IncreaseZoomCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => MapManager.ZoomIn();

        private void DecreaseZoomCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => MapManager.ZoomOut();

        private void HelpCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            => MessageBox.Show(DataUtilities.Help, "Help");

        private void About_Click(object sender, RoutedEventArgs e)
            => MessageBox.Show(DataUtilities.About, "About");

        private void Resize_Click(object sender, RoutedEventArgs e)
            => MapManager.Resize();
    }
}
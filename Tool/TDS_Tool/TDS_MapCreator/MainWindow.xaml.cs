//Author: Dominik Dohmeier
using System.Windows;
using System.Windows.Controls;

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
            MapManager = new MapManager(MapGrid, MapWidth, MapHeight, MapScale);
        }

        private void LoadMap_Click(object sender, RoutedEventArgs e) 
            => MapManager.LoadMap();

        private void Quit_Click(object sender, RoutedEventArgs e) 
            => Application.Current.Shutdown();

        private void SaveMap_Click(object sender, RoutedEventArgs e) 
            => MapManager.SaveMap();

        private void NewMap_Click(object sender, RoutedEventArgs e) 
            => MapManager.CreateNewMap();

        private void ShowGridlines_Click(object sender, RoutedEventArgs e) 
            => MapGrid.ShowGridLines = ((MenuItem)sender).IsChecked;           
    }
}
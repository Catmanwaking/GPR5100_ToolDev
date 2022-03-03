//Author: Dominik Dohmeier
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TDS_MapCreator.Tiles;

//Author: Dominik Dohmeier
namespace TDS_MapCreator
{
    public class MapManager
    {
        private const int TILE_SIZE = 16;
        private const int MIN_WIDTH = 15;
        private const int MIN_HEIGHT = 10;
        private const int MIN_SCALE = 1;
        private const int MAX_WIDTH = 60;
        private const int MAX_HEIGHT = 40;
        private const int MAX_SCALE = 10;

        private int tileScale = 2;

        private TileContent[,] mapTiles;

        private readonly Grid mapGrid;
        private readonly TextBox widthTextBox;
        private readonly TextBox heightTextBox;
        private readonly TextBox scaleTextBox;

        private Image[,] tileImages;
        private Image[,] buildingImages;
        private Image[,] unitImages;

        public MapManager(Grid grid, TextBox width, TextBox height, TextBox scale)
        {
            mapGrid = grid;

            widthTextBox = width;
            widthTextBox.Text = MIN_WIDTH.ToString();
            widthTextBox.LostFocus += WidthTextBox_LostFocus;

            heightTextBox = height;
            heightTextBox.Text = MIN_HEIGHT.ToString();
            heightTextBox.LostFocus += HeightTextBox_LostFocus;

            scaleTextBox = scale;
            scaleTextBox.Text = tileScale.ToString();
            scaleTextBox.LostFocus += ScaleTextBox_LostFocus;
            scaleTextBox.KeyDown += ScaleTextBox_KeyDown;

            CreateNewMap();
        }

        private void PlaceTile(int posX, int posY)
        {
            int tileIndex = MainWindow.TileSeletionManager.GetTileIndex();
            PlaceTile(posX, posY, tileIndex);

            //TODO check if building or unit needs to be removed
        }

        private void PlaceTile(int posX, int posY, int tileIndex)
        {
            mapTiles[posX, posY].TileID = tileIndex;
            tileImages[posX, posY].Source = new BitmapImage(DataUtilities.TileSprites[tileIndex]);
        }

        private void PlaceBuilding(int posX, int posY)
        {
            int tileIndex = MainWindow.TileSeletionManager.GetTileIndex();
            int colorIndex = MainWindow.TileSeletionManager.GetColorIndex();
            int buildingID = (int)BuildingType.None;

            if (tileIndex == (int)BuildingType.None)
                PlaceBuilding(posX, posY, buildingID);
            else
            {
                if (tileIndex == (int)BuildingType.HQ && colorIndex == 0)
                    return;

                PlaceTile(posX, posY, (int)TileType.Grass);
                buildingID = tileIndex + (colorIndex * DataUtilities.BUILDING_COUNT);

                if (DataUtilities.IsHQ(buildingID))
                    SearchAndRemove(buildingID);

                PlaceBuilding(posX, posY, buildingID);
            }
        }

        private void PlaceBuilding(int posX, int posY, int buildingIndex)
        {
            mapTiles[posX, posY].BuildingID = buildingIndex;

            if (buildingIndex == 0)
                buildingImages[posX, posY].Source = null;
            else
                buildingImages[posX, posY].Source = new BitmapImage(DataUtilities.BuildingSprites[buildingIndex - 2]);
        }

        private void PlaceUnit(int posX, int posY)
        {
            int tileIndex = MainWindow.TileSeletionManager.GetTileIndex();
            int colorIndex = MainWindow.TileSeletionManager.GetColorIndex();
            int unitID = (int)UnitType.None;

            if (tileIndex == (int)UnitType.None)
                PlaceUnit(posX, posY, unitID);
            else
            {
                unitID = tileIndex + (colorIndex * DataUtilities.UNIT_COUNT);
                PlaceUnit(posX, posY, unitID);
            }
        }

        private void PlaceUnit(int posX, int posY, int UnitIndex)
        {
            mapTiles[posX, posY].UnitID = UnitIndex;
            if (UnitIndex == 0)
                unitImages[posX, posY].Source = null;
            else
                unitImages[posX, posY].Source = new BitmapImage(DataUtilities.UnitSprites[UnitIndex - 1]);
        }

        private void SearchAndRemove(int buildingID)
        {
            for (int x = 0; x < mapTiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapTiles.GetLength(1); y++)
                {
                    if (mapTiles[x, y].BuildingID == buildingID)
                        PlaceBuilding(x, y, (int)BuildingType.None);
                }
            }
        }

        private void WidthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ClampTextBoxValue(widthTextBox, MIN_WIDTH, MAX_WIDTH, MIN_WIDTH);
            e.Handled = true;
        }

        private void HeightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ClampTextBoxValue(heightTextBox, MIN_HEIGHT, MAX_HEIGHT, MIN_HEIGHT);
            e.Handled = true;
        }

        private void ScaleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ClampTextBoxValue(scaleTextBox, MIN_SCALE, MAX_SCALE, tileScale);
            tileScale = int.Parse(scaleTextBox.Text);
            Rescale();
            e.Handled = true;
        }

        private void ScaleTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ClampTextBoxValue(scaleTextBox, MIN_SCALE, MAX_SCALE, tileScale);
                tileScale = int.Parse(scaleTextBox.Text);
                Rescale();
            }
        }

        private void ClampTextBoxValue(TextBox box, int min, int max, int resetValue)
        {
            if (int.TryParse(box.Text, out int value))
            {
                if (value < min)
                    box.Text = min.ToString();
                else if (value > max)
                    box.Text = max.ToString();
            }
            else
                box.Text = resetValue.ToString();
        }

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                Image_MouseDown(sender, null);
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameworkElement block = (FrameworkElement)sender;

            int typeIndex = MainWindow.TileSeletionManager.GetTypeIndex();

            int posX = Grid.GetColumn(block);
            int posY = Grid.GetRow(block);
            switch (typeIndex)
            {
                case 0:
                    PlaceTile(posX, posY);
                    break;

                case 1:
                    PlaceBuilding(posX, posY);
                    break;

                case 2:
                    PlaceUnit(posX, posY);
                    break;
            }
        }

        private void CreateGrid()
        {
            int width = mapTiles.GetLength(0);
            int height = mapTiles.GetLength(1);

            mapGrid.Children.Clear();
            mapGrid.ColumnDefinitions.Clear();
            mapGrid.RowDefinitions.Clear();

            tileImages = new Image[width, height];
            buildingImages = new Image[width, height];
            unitImages = new Image[width, height];

            int size = TILE_SIZE * tileScale;
            mapGrid.Width = size * mapTiles.GetLength(0);
            mapGrid.Height = size * mapTiles.GetLength(1);

            for (int i = 0; i < mapTiles.GetLength(0); i++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }
            for (int i = 0; i < mapTiles.GetLength(1); i++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }

            for (int x = 0; x < mapTiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapTiles.GetLength(1); y++)
                    CreateGridContent(x, y);
            }
        }

        private void CreateGridContent(int posX, int posY)
        {
            int size = TILE_SIZE * tileScale;

            Image tileImage = new Image
            {
                Width = size,
                Height = size,
            };
            SetImageValues(tileImage, posX, posY, 1);

            Image buildingImage = new Image
            {
                Width = size >> 1,
                Height = size,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            SetImageValues(buildingImage, posX, posY, 2);

            Image unitImage = new Image
            {
                Width = size >> 1,
                Height = size >> 1,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            SetImageValues(unitImage, posX, posY, 3);

            tileImages[posX, posY] = tileImage;
            buildingImages[posX, posY] = buildingImage;
            unitImages[posX, posY] = unitImage;

            TileContent tile = mapTiles[posX, posY];
            PlaceTile(posX, posY, tile.TileID);
            PlaceBuilding(posX, posY, tile.BuildingID);
            PlaceUnit(posX, posY, tile.UnitID);
        }

        private void SetImageValues(Image image,int posX, int posY, int Zindex)
        {
            image.Stretch = Stretch.Uniform;
            image.MouseDown += Image_MouseDown;
            image.MouseEnter += Image_MouseEnter;
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);
            Grid.SetColumn(image, posX);
            Grid.SetRow(image, posY);
            Panel.SetZIndex(image, Zindex);
            _ = mapGrid.Children.Add(image);
        }

        public void CreateNewMap()
        {
            int width = int.Parse(widthTextBox.Text);
            int height = int.Parse(heightTextBox.Text);
            if (width < MIN_WIDTH || height < MIN_HEIGHT || width > MAX_WIDTH || height > MAX_HEIGHT)
            {
                //USER FRIENDLY ERROR?
                return;
            }

            mapTiles = new TileContent[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    mapTiles[x, y] = new TileContent()
                    {
                        TileID = (int)TileType.Grass,
                        BuildingID = (int)BuildingType.None,
                        UnitID = (int)UnitType.None
                    };
                }
            }

            CreateGrid();
        }

        private void Rescale()
        {
            int size = TILE_SIZE * tileScale;
            mapGrid.Width = size * mapTiles.GetLength(0);
            mapGrid.Height = size * mapTiles.GetLength(1);
            for (int x = 0; x < mapTiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapTiles.GetLength(1); y++)
                {
                    tileImages[x, y].Width = size;
                    tileImages[x, y].Height = size;

                    buildingImages[x, y].Width = size >> 1;
                    buildingImages[x, y].Height = size;

                    unitImages[x, y].Width = size >> 1;
                    unitImages[x, y].Height = size >> 1;
                }
            }
        }

        public void LoadMap()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text Document|*.txt";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                string jsonString = File.ReadAllText(openFileDialog.FileName);

                mapTiles = (TileContent[,])JsonConvert.DeserializeObject(jsonString, typeof(TileContent[,]));
                CreateGrid();
            }
        }

        public void SaveMap()
        {
            string jsonString = JsonConvert.SerializeObject(mapTiles);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "Text Document|*.txt";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, jsonString);
        }
    }
}
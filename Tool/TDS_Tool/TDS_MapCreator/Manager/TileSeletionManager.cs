//Author: Dominik Dohmeier
using System.Windows.Controls;
using TDS_MapCreator.Tiles;

namespace TDS_MapCreator
{
    public class TileSeletionManager
    {
        private readonly ComboBox tileTypeSelector;
        private readonly ComboBox tileSelector;
        private readonly ComboBox colorSelector;

        public TileSeletionManager(ComboBox tileTypeSelector, ComboBox tileSelector, ComboBox colorSelector)
        {
            this.tileTypeSelector = tileTypeSelector;
            this.tileSelector = tileSelector;
            this.colorSelector = colorSelector;

            this.tileTypeSelector.SelectionChanged += TileTypeSelector_SelectionChanged;
            this.tileTypeSelector.SelectedIndex = 0;
        }

        public int GetTypeIndex()
        {
            return tileTypeSelector.SelectedIndex;
        }

        public int GetTileIndex()
        {
            return tileSelector.SelectedIndex;
        }

        public int GetColorIndex()
        {
            return colorSelector.SelectedIndex;
        }

        private void TileTypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Type type;
            System.Type colorType;
            switch (tileTypeSelector.SelectedIndex)
            {
                case 0:
                    type = typeof(TileType);
                    colorType = null;
                    break;

                case 1:
                    type = typeof(BuildingType);
                    colorType = typeof(BuildingColors);
                    break;

                case 2:
                    type = typeof(UnitType);
                    colorType = typeof(UnitColors);
                    break;

                default:
                    return;
            }

            string[] types = System.Enum.GetNames(type);

            tileSelector.ItemsSource = types;
            tileSelector.SelectedIndex = 0;

            string[] colors = default;
            if (colorType != null)
            {
                colors = System.Enum.GetNames(colorType);
            }
            colorSelector.ItemsSource = colors;
            colorSelector.SelectedIndex = 0;
        }
    }
}
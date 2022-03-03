//Author: Dominik Dohmeier
using System;
using System.IO;
using System.Linq;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace TDS_MapCreator.Tiles
{
    public enum UnitColors
    {
        Red = 0,
        Black,
    }

    public enum BuildingColors
    {
        Neutral = 0,
        Red,
        Black,
    }

    public enum TileType
    {
        Grass = 0,
        Forest,
        Mountains,
        Street,
        Bridge,
        River,
        Water,
        Riffs,
    }

    public enum BuildingType
    {
        None = 0,
        HQ,
        Town,
        Base,
        Airport,
        Harbor
    }

    public enum UnitType
    {
        None = 0,
        Infantry,
        Mech,
        Recon,
        Tank,
        Medium_Tank,
        Neotank,
        APC,
        Artillery,
        Rocket,
        Anti_Air,
        Missiles,
        Lander,
        Cruiser,
        Submarine,
        Battleship,
        B_Copter,
        T_Copter,
        Fighter,
        Bomber,
    }

    internal static class DataUtilities
    {
        public const int TILE_COUNT = 8;
        public const int BUILDING_COUNT = 5;
        public const int UNIT_COUNT = 19;

        private static readonly string spriteDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Sprites");

        private static readonly int[] HQIDs =
        {
            ((int)BuildingType.HQ) + (((int)BuildingColors.Red) * BUILDING_COUNT),
            ((int)BuildingType.HQ) + (((int)BuildingColors.Black) * BUILDING_COUNT),
        };

        public static readonly Uri[] TileSprites =
        {
            new Uri($"{spriteDirectory}\\Tiles\\Plains.png"),
            new Uri($"{spriteDirectory}\\Tiles\\Forest.png"),
            new Uri($"{spriteDirectory}\\Tiles\\Mountain.png"),
            new Uri($"{spriteDirectory}\\Tiles\\Street.png"),
            new Uri($"{spriteDirectory}\\Tiles\\Bridge.png"),
            new Uri($"{spriteDirectory}\\Tiles\\River.png"),
            new Uri($"{spriteDirectory}\\Tiles\\Water.png"),
            new Uri($"{spriteDirectory}\\Tiles\\Riffs.png"),
        };

        public static readonly Uri[] UnitSprites =
        {
            new Uri($"{spriteDirectory}\\Units\\Red\\Infantry.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Mech.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Recon.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Tank.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Medium_Tank.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Neotank.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\APC.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Artillery.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Rocket.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Anti-Air.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Missiles.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Lander.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Cruiser.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Submarine.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Battleship.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\B_Copter.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\T_Copter.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Fighter.png"),
            new Uri($"{spriteDirectory}\\Units\\Red\\Bomber.png"),

            new Uri($"{spriteDirectory}\\Units\\Black\\Infantry.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Mech.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Recon.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Tank.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Medium_Tank.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Neotank.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\APC.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Artillery.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Rocket.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Anti-Air.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Missiles.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Lander.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Cruiser.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Submarine.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Battleship.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\B_Copter.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\T_Copter.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Fighter.png"),
            new Uri($"{spriteDirectory}\\Units\\Black\\Bomber.png"),
        };

        public static readonly Uri[] BuildingSprites =
        {
            new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Town.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Base.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Airport.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Harbor.png"),

            new Uri($"{spriteDirectory}\\Buildings\\Red\\HQ.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Red\\Town.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Red\\Base.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Red\\Airport.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Red\\Harbor.png"),

            new Uri($"{spriteDirectory}\\Buildings\\Black\\HQ.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Black\\Town.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Black\\Base.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Black\\Airport.png"),
            new Uri($"{spriteDirectory}\\Buildings\\Black\\Harbor.png"),
        };

        public static bool IsHQ(int ID)
        {
            return HQIDs.Contains(ID);
        }
    }
}
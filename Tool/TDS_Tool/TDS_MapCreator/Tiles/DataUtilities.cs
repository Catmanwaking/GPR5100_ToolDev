//Author: Dominik Dohmeier
using System;
using System.IO;
using System.Reflection;

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
#if DEBUG
        private static readonly string spriteDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Sprites");
#else
        private static readonly string spriteDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sprites");
#endif
        public const int TILE_COUNT = 8;
        public const int BUILDING_COUNT = 5;
        public const int UNIT_COUNT = 19;

        public static readonly string Help =
            $"First Set the size of the map according to your needs and press either \"Resize\" or create a new map using \"Ctrl+N\"." +
            $"Select the tiletype you wish to place, then specify which tile of the selected tiletype you want and if necessary select a color." +
            $"Tiles have no colors, Buildings are either neutral (white), red or black and units are either red or black.\n\n" +
            $"To draw onto the tilemap simply click and drag to draw where you want to place the selected tile.\n" +
            $"To remove a tile either right click it or select \"none\" and draw over it.\n" +
            $"To zoom in or out hold Ctrl and use the mousewheel or press (+)/(-)\n\n" +
            $"There are certain rules the editor applies automatically like, you can only place one HQ of each color or tanks can't be placed on mountains." +
            $"So if something can't be placed, it's against the rules";

        public static readonly string About = $"TDS Editor version {Assembly.GetExecutingAssembly().GetName().Version}\n\n" +
            $"Advance Wars Map Editor for use with the Unity Application \"TDS\"\n" +
            $"2022 Dominik Dohmeier";

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

        public static readonly Uri[][] UnitSprites =
        {
            new []
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
            },
            new []
            {
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
            }
        };

        public static readonly Uri[][] BuildingSprites =
        {
            new []
            {
                null,
                new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Town.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Base.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Airport.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Neutral\\Harbor.png"),
            },
            new []
            {
                new Uri($"{spriteDirectory}\\Buildings\\Red\\HQ.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Red\\Town.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Red\\Base.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Red\\Airport.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Red\\Harbor.png"),
            },
            new []
            {
                new Uri($"{spriteDirectory}\\Buildings\\Black\\HQ.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Black\\Town.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Black\\Base.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Black\\Airport.png"),
                new Uri($"{spriteDirectory}\\Buildings\\Black\\Harbor.png"),
            }
        };

        public static readonly bool[,] unitTileMatrix =
        {
            //plains, forest, mountains, street, bridge, river, water, reefs
            { true, true, true, true, true, true, false, false },       //infantry
            { true, true, true, true, true, true, false, false },       //mech
            { true, true, false, true, true, false, false, false },     //recon
            { true, true, false, true, true, false, false, false },     //tank
            { true, true, false, true, true, false, false, false },     //medium-tank
            { true, true, false, true, true, false, false, false },     //neotank
            { true, true, false, true, true, false, false, false },     //apc
            { true, true, false, true, true, false, false, false },     //artillery
            { true, true, false, true, true, false, false, false },     //rocket
            { true, true, false, true, true, false, false, false },     //anti-air
            { true, true, false, true, true, false, false, false },     //missile
            { false, false, false, false, false, false, true, true },   //lander
            { false, false, false, false, false, false, true, true },   //cruiser
            { false, false, false, false, false, false, true, true },   //submarine
            { false, false, false, false, false, false, true, true },   //battleship
            { true, true, true, true, true, true, true, true },         //b_copter
            { true, true, true, true, true, true, true, true },         //t_copter
            { true, true, true, true, true, true, true, true },         //fighter
            { true, true, true, true, true, true, true, true },         //bomber
        };

        public static int ToID(int index, int colorIndex)
        {
            return (colorIndex << 5) + index;
        }

        public static int ToIndex(int ID)
        {
            return ID & 0b0001_1111;
        }

        public static int ToColorIndex(int ID)
        {
            return (ID & 0b1110_0000) >> 5;
        }
    }
}
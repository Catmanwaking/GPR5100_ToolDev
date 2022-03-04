//Author: Dominik Dohmeier
using Assets.Scripts.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private Tilemap units;

    [SerializeField] private MapDataSO mapData;
    [SerializeField] private TileSpriteDataSO spriteData;

    // Start is called before the first frame update
    private void Start()
    {
        TileContent[,] tiles = mapData.MapTiles;
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileContent tile = tiles[x, y];
                if (tile.BuildingID != 0)
                {
                    map.SetTile(new Vector3Int(x, height - y - 1),
                        GetBuildingTile(tile.BuildingID));
                }
                else
                {
                    map.SetTile(new Vector3Int(x, height - y - 1),
                        GetMapTile(tile.TileID));
                }
                if (tile.UnitID != 0)
                {
                    units.SetTile(new Vector3Int(x, height - y - 1),
                        GetUnitTile(tile.UnitID));
                }
            }
        }
        map.RefreshAllTiles();
    }

    private TileBase GetMapTile(int mapTileID)
    {
        return spriteData.MapSprites[mapTileID];
    }

    private TileBase GetBuildingTile(int buildingID)
    {
        int buildingIndex = ToIndex(buildingID);
        int colorIndex = ToColorIndex(buildingID);

        return colorIndex switch
        {
            0 => spriteData.BuildingSpritesNeutral[buildingIndex - 1],
            1 => spriteData.BuildingSpritesRed[buildingIndex - 1],
            2 => spriteData.BuildingSpritesBlack[buildingIndex - 1],
            _ => null,
        };
    }

    private TileBase GetUnitTile(int unitID)
    {
        int unitIndex = ToIndex(unitID);
        int colorIndex = ToColorIndex(unitID);

        return colorIndex switch
        {
            0 => spriteData.UnitSpritesRed[unitIndex - 1],
            1 => spriteData.UnitSpritesBlack[unitIndex - 1],
            _ => null,
        };
    }

    private int ToIndex(int ID)
    {
        return ID & 0b0001_1111;
    }

    private int ToColorIndex(int ID)
    {
        return (ID & 0b1110_0000) >> 5;
    }
}
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
                    map.SetTile(new Vector3Int(x, height - y -1), spriteData.BuildingSprites[tile.BuildingID - 2]);
                else
                    map.SetTile(new Vector3Int(x, height - y - 1), spriteData.MapSprites[tile.TileID]);

                if (tile.UnitID != 0)
                    units.SetTile(new Vector3Int(x, height - y - 1), spriteData.UnitSprites[tile.UnitID - 1]);
            }
        }
    }
}
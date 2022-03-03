//Author: Dominik Dohmeier
using UnityEngine;

namespace Assets.Scripts.Tiles
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Loader/TileMapLoader")]
    public class MapDataSO : ScriptableObject
    {
        public TileContent[,] MapTiles;
    }
}
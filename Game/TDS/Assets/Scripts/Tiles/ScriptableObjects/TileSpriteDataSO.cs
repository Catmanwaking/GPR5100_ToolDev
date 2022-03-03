//Author: Dominik Dohmeier
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    [CreateAssetMenu(fileName = "TileSpriteData", menuName ="Loader/TileSpriteData")]
    public class TileSpriteDataSO : ScriptableObject
    {
        public TileBase[] MapSprites;
        public TileBase[] BuildingSprites;
        public TileBase[] UnitSprites;
    }
}
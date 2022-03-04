//Author: Dominik Dohmeier
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    [CreateAssetMenu(fileName = "TileSpriteData", menuName ="Loader/TileSpriteData")]
    public class TileSpriteDataSO : ScriptableObject
    {
        public TileBase[] MapSprites;
        public TileBase[] BuildingSpritesNeutral;
        public TileBase[] BuildingSpritesRed;
        public TileBase[] BuildingSpritesBlack;
        public TileBase[] UnitSpritesRed;
        public TileBase[] UnitSpritesBlack;
    }
}
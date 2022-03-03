using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="CustomRuleTile",menuName ="2D/Tiles/Custom Rule Tiles/Connection Rule Tile")]
public class ConnectionRuleTile : RuleTile<ConnectionRuleTile.Neighbor>
{
    [SerializeField] private TileBase[] tilesToConnect;
    [SerializeField] private TileBase[] tilesToIgnore;

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        return neighbor switch
        {
            Neighbor.This => Check_This(tile),
            Neighbor.NotThis => Check_NotThis(tile),
            Neighbor.Specified => Check_Specified(tile),
            _ => base.RuleMatch(neighbor, tile),
        };
    }

    private bool Check_This(TileBase tile)
    {
        return tile == null || tilesToConnect.Contains(tile) || tile == this || tilesToIgnore.Contains(tile);
    }

    private bool Check_NotThis(TileBase tile)
    {
        return tile != null && !tilesToIgnore.Contains(tile) && tile != this;
    }

    private bool Check_Specified(TileBase tile)
    {
        return tilesToConnect.Contains(tile);
    }

    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Specified = 3;
    }
}
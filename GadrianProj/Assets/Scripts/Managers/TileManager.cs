using UnityEngine;
using System.Collections.Generic;

public class TileManager : Singleton<TileManager>
{
    private Dictionary<Vector3, int> tilesPosition;

    public Dictionary<Vector3, int> TilesPosition
    {
        get
        {
            if ( tilesPosition == null )
            {
                tilesPosition = new Dictionary<Vector3, int>();
            }
            return tilesPosition;
        }
    }

    public Vector3 GetFreeTilePosition ()
    {
        foreach ( KeyValuePair<Vector3, int> tile in TilesPosition )
        {
            if ( tile.Value != -1 )
            {
                TilesPosition[tile.Key] = -1;
                return tile.Key;
            }
        }
        return Vector3.zero;
    }
}

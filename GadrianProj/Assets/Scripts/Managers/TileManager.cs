using UnityEngine;
using System.Collections.Generic;

public class TileManager : Singleton<TileManager>
{
    private Dictionary<Vector3, bool> tilesPosition;

    public Dictionary<Vector3, bool> TilesPosition
    {
        get
        {
            if ( tilesPosition == null )
            {
                tilesPosition = new Dictionary<Vector3, bool>();
            }
            return tilesPosition;
        }
    }

    public Vector3 GetFreeTilePosition ()
    {
        foreach ( KeyValuePair<Vector3, bool> tile in TilesPosition )
        {
            if ( tile.Value == false )
            {
                TilesPosition[tile.Key] = true;
                return tile.Key;
            }
        }
        return Vector3.zero;
    }
}

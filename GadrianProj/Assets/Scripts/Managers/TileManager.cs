using UnityEngine;
using System.Collections.Generic;

public class TileManager : Singleton<TileManager>
{
    private Dictionary<Vector3, int> tilesPosition; // The vector3 key is on grid coordinates

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

    public TileConfiguration GetNextCharacterTile ()
    {
        foreach ( KeyValuePair<Vector3, int> tile in TilesPosition )
        {
            if ( tile.Value != -1 )
            {
                TileConfiguration tileConf = new TileConfiguration(tile.Key, tile.Value);
                TilesPosition[tile.Key] = -1;
                return tileConf;
            }
        }
        return null;
    }
}

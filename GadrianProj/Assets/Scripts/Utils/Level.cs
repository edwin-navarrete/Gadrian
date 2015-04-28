using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level : ScriptableObject
{
    public List<Vector2> tilesPosition;

    public void AddTilePosition (Vector2 newPosition)
    {
        if ( tilesPosition == null )
        {
            tilesPosition = new List<Vector2>();
        }
        tilesPosition.Add( newPosition );
    }
}

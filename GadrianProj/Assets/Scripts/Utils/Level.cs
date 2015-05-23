using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level : ScriptableObject
{
    public List<Vector3> tilesPosition;

    public void AddTilePosition (Vector2 position, int personalityIndex)
    {
        if ( tilesPosition == null )
        {
            tilesPosition = new List<Vector3>();
        }
		Vector3 newTileConfig = new Vector3(position.x, position.y, personalityIndex);
        tilesPosition.Add( newTileConfig );
    }
}

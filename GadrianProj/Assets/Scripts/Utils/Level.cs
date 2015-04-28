using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level : ScriptableObject
{
    public List<Vector2> tilesPosition;

    public void OnEnable ()
    {
        tilesPosition = new List<Vector2>();
    }

    public void AddTilePosition (Vector2 newPosition)
    {
        tilesPosition.Add( newPosition );
    }
}

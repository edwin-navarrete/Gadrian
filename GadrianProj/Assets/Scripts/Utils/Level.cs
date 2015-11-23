using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level : ScriptableObject
{
    public int startingMoves;
    public List<TileConfiguration> tilesPosition;

    public void AddTilePosition(Vector2 position, int personalityIndex)
    {
        if ( tilesPosition == null )
        {
            tilesPosition = new List<TileConfiguration>();
        }
        TileConfiguration tile = new TileConfiguration( position, personalityIndex );
        tilesPosition.Add( tile );
    }
}

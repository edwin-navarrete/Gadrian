using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> tilesPrefab;

	[SerializeField]
	private GFGrid grid;

	public void Awake ()
	{
        grid = GridManager.Instance.Grid;
	}

	public void Start ()
	{
        int moves = LoadLevel();
        Generatelevel();
        EventManager.TriggerEvent(Events.LevelGenerated, moves);
    }

    private int LoadLevel ()
    {
        int levelToLoad = PlayerPrefs.GetInt( Strings.LevelToLoad, 0 );
        string fileName = string.Format( Strings.GenericLevelName, levelToLoad );
        Level level = Resources.Load<Level>( Strings.LevelPath + fileName );

        if ( level != null )
        {
            foreach ( TileConfiguration tileConfiguration in level.tilesPosition )
            {
                TileManager.Instance.TilesPosition.Add( tileConfiguration.position, tileConfiguration.personalityIndex );
            }
            return level.startingMoves;
        }
        else
        {
            PlayerPrefs.SetInt(Strings.LevelToLoad, 0);
            Debug.LogError( "The variable level could not load successfully the resource, reseted to zero" );
        }
        return 0;
    }

	private void Generatelevel ()
	{
        
        foreach ( KeyValuePair<Vector3, int> position in TileManager.Instance.TilesPosition )
		{
			int cellColor = GetCellColor(position.Key);

			GameObject newCell = Instantiate ( tilesPrefab[cellColor] ) as GameObject;
			Vector3 worldVector = grid.GridToWorldFixed( (Vector2) position.Key);
			newCell.transform.position = worldVector;
			newCell.transform.SetParent ( this.transform );
		}
	}

	/**
	 * Distribuir los tres colores en la grilla hexagonal de tal manera que 
	 * el mismo color no está nunca en celdas contiguas.
	 */
	private int GetCellColor(Vector3 gridPos){

		float p = gridPos.x / 3f;
		float q = gridPos.y / 2f;
		float r = (gridPos.x - 1f) / 3f;
		float s = (gridPos.y - 1f) / 2f;		
		float t = (gridPos.x + 1f) / 3f;

		if( (p + q) % 1 == 0 || (r + s) % 1 == 0 ){
			return 0;
		}
		else if( (t + q) % 1 == 0 || (p + s) % 1 == 0 ){
			return 1;
		}

		return 2;
	}
}

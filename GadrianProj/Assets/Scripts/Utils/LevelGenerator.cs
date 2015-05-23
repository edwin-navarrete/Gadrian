﻿using UnityEngine;
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
        LoadLevel();

        Generatelevel(); 
    }

    private void LoadLevel ()
    {
        //TODO Create a GameManager to hold on a level index variable or create an object to make data persist through scenes
        string fileName = string.Format( "level{0}", 0 );
        Level level = Resources.Load<Level>( "Levels/" + fileName );

        if ( level != null )
        {
            foreach ( Vector3 gridVector in level.tilesPosition )
            {
                TileManager.Instance.TilesPosition.Add( gridVector, (int) gridVector.z );
            }
        }
        else
        {
            Debug.LogError( "The variable level could not load successfully the resource" );
        }
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

        EventManager.TriggerEvent( "LevelGenerated" );
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

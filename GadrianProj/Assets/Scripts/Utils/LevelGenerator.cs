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
            foreach ( Vector2 gridVector in level.tilesPosition )
            {
                Vector3 worldVector = grid.GridToWorldFixed( gridVector );
                TileManager.Instance.TilesPosition.Add( worldVector, false );
            }
        }
        else
        {
            Debug.LogError( "The variable level could not load successfully the resource" );
        }
    }

	private void Generatelevel ()
	{
        int index = 0;
        foreach ( KeyValuePair<Vector3, bool> position in TileManager.Instance.TilesPosition )
		{
			GameObject newCell = Instantiate ( tilesPrefab[index] ) as GameObject;
            newCell.transform.position = position.Key;
			newCell.transform.SetParent ( this.transform );
            index++;

            if ( index >= tilesPrefab.Count )
            {
                index = 0;
            }
		}

        EventManager.TriggerEvent( "LevelGenerated" );
	}
}

using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> tilesPrefab;

	[SerializeField]
	private GFGrid grid;

	private List<Vector2> cellsPosition;

	public void Awake ()
	{
        grid = GridManager.Instance.Grid;
        cellsPosition = new List<Vector2>();
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
            Debug.Log( "Loading level resourse complete" );
            foreach ( Vector2 vector in level.tilesPosition )
            {
                Debug.LogFormat( "Adding tile position at:{0}", vector );
                cellsPosition.Add( vector );
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
		foreach ( Vector2 position in cellsPosition )
		{
			GameObject newCell = Instantiate ( tilesPrefab[index] ) as GameObject;
            Vector3 newPosition = grid.GridToWorldFixed ( position );
			newCell.transform.position = newPosition;
			newCell.transform.SetParent ( this.transform );
            index++;

            if ( index >= tilesPrefab.Count )
            {
                index = 0;
            }
		}
	}
}

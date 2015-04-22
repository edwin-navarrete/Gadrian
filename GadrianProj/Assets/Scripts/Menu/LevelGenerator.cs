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
		grid = GameObject.FindGameObjectWithTag ( "Grid" ).GetComponent<GFGrid> ();
        cellsPosition = new List<Vector2>();
	}

	public void Start ()
	{
        //TODO Create a GameManager to hold on a level index variable or create an object to make data persist through scenes
        string fileName = string.Format( "level{0}.level", 0 );
        StreamReader sr = new StreamReader( Application.dataPath + "/Levels/" + fileName );
        string levelFile = sr.ReadToEnd();
        string[] vectors = levelFile.Split( ',' );

        foreach ( string vector in vectors )
        {
            string[] axis = vector.Split( ' ' );
            cellsPosition.Add( new Vector2( int.Parse( axis[0] ), int.Parse( axis[1] ) ) );
        }

		Generatelevel ();
	}

	private void Generatelevel ()
	{
        int index = 0;
		foreach ( Vector2 position in cellsPosition )
		{
			GameObject newCell = Instantiate ( tilesPrefab[index] ) as GameObject;
            Vector3 newPosition = grid.GridToWorld ( position );
            Vector3 modifiedPositon = new Vector3( newPosition.x * 0.75f, newPosition.y * 1.15f, newPosition.z );
			newCell.transform.position = modifiedPositon;
			newCell.transform.SetParent ( this.transform );
            index++;

            if ( index >= tilesPrefab.Count )
            {
                index = 0;
            }
		}
	}
}

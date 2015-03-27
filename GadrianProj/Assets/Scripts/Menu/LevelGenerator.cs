using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField]
	private GameObject cellPrefab;

	[SerializeField]
	private GFGrid grid;

	private List<Vector2> cellsPosition;

	public void Awake ()
	{
		grid = GameObject.FindGameObjectWithTag ( "Grid" ).GetComponent<GFGrid> ();
	}

	public void Start ()
	{
		cellsPosition = new List<Vector2> () {
			new Vector2 ( 0, 0 ),
			new Vector2 ( 1, 0 ),
			new Vector2 ( -1, 0 ),
			new Vector2 ( 0, -1 ),
			new Vector2 ( -1, -1 ),
			new Vector2 ( -1, 1 ),
			new Vector2 ( 0, 1 )
		};

		Generatelevel ();
	}

	private void Generatelevel ()
	{
		foreach ( Vector2 position in cellsPosition )
		{
			GameObject newCell = Instantiate ( cellPrefab ) as GameObject;
			newCell.transform.position = grid.GridToWorld ( position );
			newCell.transform.SetParent ( this.transform );
		}
	}
}

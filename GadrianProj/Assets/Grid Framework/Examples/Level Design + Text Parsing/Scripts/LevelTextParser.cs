using UnityEngine;
using System.Collections.Generic; //needed for the generic list class
using System.IO;                  //needed for the StringReader class

[RequireComponent(typeof(GFGrid))]
public class LevelTextParser : MonoBehaviour {
	/// <summary>An array of text files to be read.</summary>
	public TextAsset[] levelData;
		
	// Prefabs for our objects
	public GameObject red;   ///< <summary>Prefab for the red object.  </summary>
	public GameObject green; ///< <summary>Prefab for the green object.</summary>
	public GameObject blue;  ///< <summary>Prefab for the blue object. </summary>
	
	/// <summary>The grid we place blocks on.</summary>
	private GFGrid levelGrid;
	/// <summary>This object is what reads the text file.</summary>
	private StringReader reader;
	
	/// <summary>Which level from the levels array to load.</summary>
	private int currentLevel;
	/// <summary>In order to delete all the blocks we need to keep track of them.</summary>
	private List<GameObject> blocks;
	/// <summary>The shift from the coordinate we get (depends on the type of grid).</summary>
	private Vector3 offset;
	
	/// <summary>Possible button positions.</summary>
	public enum ButtonPosition {UpLeft, DownLeft, DownRight};
	/// <summary>Position of the button for this particular instance.</summary>
	public ButtonPosition buttonPosition;
	
	public void Awake(){
		levelGrid = GetComponent<GFGrid>();
		blocks = new List<GameObject>();

		// if and how much we need to shift the objects depends on the type of grid
		// This just just a matter of different grid coordinate systems. Try playing
		// with the numbers to see the difference.
		offset = Vector3.zero;
		// offset by 0.5 horizontal and vertical in rectangular grids
		if (levelGrid.GetType() == typeof(GFRectGrid) ) { offset.x += 0.5f; offset.y -= 0.5f; }
		// Add 1 for polar grids because we don't want the origin
		if (levelGrid.GetType() == typeof(GFPolarGrid)) { offset.x += 1; }

		BuildLevel(levelData[currentLevel], levelGrid);
	}

	/// <summary>Spawns blocks based on a text file and a grid.</summary>
	public void BuildLevel(TextAsset levelData, GFGrid levelGrid){
		// abort if there are no prefabs to instantiate
		if(!red || !green || !blue) {return;}
		
		// loop though the list of old blocks and destroy all of them, we don't want the new level on top of the old one
		foreach(GameObject go in blocks){ if(go) {Destroy(go);}}

		// destroying the blocks doesn't remove the reference to them in the list, so clear the list
		blocks.Clear();
		
		//setup the reader, a variable for storing the read line and keep track of the number of the row we just read
		reader = new StringReader(levelData.text);
		string line;
		int row = 0;
		
		//read the text file line by line as long as there are lines
		while((line = reader.ReadLine()) != null){
			//read each line character by character
			for(int column = 0; column < line.Length; column++){
				Vector3 targetPosition = levelGrid.GridToWorld(new Vector3(column, -row, 0) + offset);
				CreateBlock(line[column], targetPosition);
			}
			//we read a row, now it's time to read the next one; increment the counter
			row++;
		}
	}
	
	/// <summary>Spawn a block in the level.</summary>
	void CreateBlock(char letter, Vector3 targetPosition){
		GameObject spawn = null;
		//set the value of cube based on the supplied character
		switch(letter){
			case 'R': { spawn = red;   break;}
			case 'G': { spawn = green; break;}
			case 'B': { spawn = blue;  break;}
		}
		//instantiate the cube if one was picked, else don't do anything
		if(spawn){
			var obj = Instantiate(spawn, targetPosition, Quaternion.identity) as GameObject;
			//add that cube into our list of blocks
			blocks.Add(obj);
		}
	}
	
	/// <summary>This function creates a GUI button that lets you switch levels.</summary>
	void OnGUI(){
		float top = buttonPosition == ButtonPosition.UpLeft ? 0 : Screen.height - 50;
		float left = buttonPosition == ButtonPosition.DownRight ? Screen.width - 170 : 0;
		if(GUI.Button(new Rect(left + 10, top - 10, 150, 50), "Try Another Level")){
			//increment the level counter; using % makes the number revert back to 0 once we have reached the limit
			currentLevel = (currentLevel + 1) % levelData.Length;
			//now build the level (BuildLevel uses the blocks variable to find and destroy any previous blocks)
			BuildLevel(levelData[currentLevel], levelGrid);
		}
	}
}

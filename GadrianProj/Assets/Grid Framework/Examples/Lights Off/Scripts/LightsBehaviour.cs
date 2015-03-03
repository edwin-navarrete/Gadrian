using UnityEngine;

/*
	ABOUT THIS SCRIPT
	
This script performs two tasks. For one, if the object it is attached to
gets clicked it will fire off an event (defined in another script) which
will be handled by this script as well. When the event is received the
script compares the object's grid coordinates and the coordinates of the
object that was clicked to decide what to do next.
This example uses delegates and events. While it would have been possible
to do this in another way, delegates and events are the most elegant and
performant solution, since they are native to .NET

This script demonstrates how you can use grid coordinates for game logic
with tiles that seem to work together but don't actually know anything
about each other, giving you great freedom in designing your levels.
*/

public class LightsBehaviour : MonoBehaviour {
	
	/// <summary>Material to use when the light is *on*.</summary>
	public Material onMaterial;
	/// <summary>Material to use when the light is *off*.</summary>
	public Material offMaterial;
	
	/// <summary>Current state of the switch.</summary>
	/// The state of the switch (intial set is done in the editor,rest at runtime)
	public bool isOn = false;
	
	/// <summary>The grid we want to use for our game logic.</summary>
	public GFGrid connectedGrid;
	
	void Awake(){		
		//perform an initial light setting
		SwitchLights();
	}
	
	void OnEnable(){
		//subscribe to the event
		SwitchManager.OnHitSwitch += OnHitSwitch;
	}
	
	void OnDisable(){
		//unsubscribe from the event
		SwitchManager.OnHitSwitch -= OnHitSwitch;
	}
	
	/// <summary>Gets called upon the event `OnHitSwitch`</summary>
	/// <param name="switchPosition">
	/// 	Position of the clicked switch in grid coordinates.
	/// </param>
	/// <param name="theGrid">The grid we use for reference.</param>
	void OnHitSwitch (Vector3 switchPosition, GFGrid theGrid){
		//don't do anything if this light doesn't belong to the grid we use
		if(theGrid != connectedGrid) {return;}
		
		//check if this light is adjacent to the switch; this is an extenion method
		//that always picks the method that belongs to the specific grid type. The
		//implementation is in another file
		if(theGrid.IsAdjacent(transform.position, switchPosition)){
			//flip the state of this switch
			isOn = !isOn;
		}
		//change the lights (won't do anything if the state hasn't changed)
		SwitchLights();
	}
	
	/// <summary>Toggles the material of the ligh.</summary>
	public void SwitchLights(){
		GetComponent<Renderer>().material = isOn ? onMaterial : offMaterial;
	}
	
	void OnMouseUpAsButton(){
		//we don't need an instance here, because this function is static
		SwitchManager.SendSignal(transform.position, connectedGrid);
	}
}

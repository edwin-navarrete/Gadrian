using UnityEngine;

/*
	ABOUT THIS SCRIPT
	
This script declares a delegate and defines an event. The event will be fired
when a switch is clicked and passes the grid as well as the switch's
coordinates in grid space. Other than that it has nothing to do with grid
Framework. The event is handled and fired in a seperate script.

*/

public static class SwitchManager{
	
	/// <summary>Declare a delegate to react when a switch is pressed.</summary>
	/// The delegate passes arguments the coordinates of the switch
	/// and which grid to use.
	public delegate void SwitchingHandler(Vector3 switchCoordinates, GFGrid theGrid);
	public static event SwitchingHandler OnHitSwitch;
	
		
	/// <summary></summary>
	/// <param name="theSwitch">Position of the switch that was pressed.</param>
	/// <param name="referenceGrid">Grid we use for gameplay.</param>
	/// This function broadcasts a signal (an event) once a switch has been hit.
	/// Static means we don't need to use any specific instance of this function.
	public static void SendSignal(Vector3 theSwitch, GFGrid referenceGrid){
		//always make sure there a subscribers to the event, or you get errors
		if(OnHitSwitch != null)
			OnHitSwitch(theSwitch, referenceGrid);
	}
}

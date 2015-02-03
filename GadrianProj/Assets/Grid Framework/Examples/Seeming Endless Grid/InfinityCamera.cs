using UnityEngine;


/// <summary>In this example we adjust the grid's rendering range at runtime to
/// the view rectangle or the main camera.</summary>
/// 
/// Rendering a very large amount of grid lines can get expensive, and when the
/// player will only ever see just a small portion of the grid it's a complete
/// waste of resources. We will instead fake the illustion of an infinitely
/// large grid by dynamically adjusting the rendering range as the camera is
/// moved.
/// 
/// The view rectangle of an orthographic camera is defined by the camera's
/// orthographic size and its aspect ratio. The orthographic size is the height
/// of the rectangle in world units and the width is comuted by multiplying the
/// height with the aspect ratio.
/// 
/// <code>width = height * aspectRatio</code>
/// 
/// The postion of the centre of the rectangle is the position of the camera in
/// the world. We need the lower left-hand and upper righ-hand corners of the
/// rectangle as the rendering range of out grid.
/// 
/// We will go slightly beyond the range of the camera for optimisation
/// purposed. Every time the range is changed the grid lines need to be
/// re-calculated. By going slightly beyond the view rectangle we give the
/// camera an are of tolerance where it can move without triggering
/// re-calculation.
/// 
/// Reading topics of interest: <a href="http://docs.unity3d.com/ScriptReference/Camera-orthographicSize.html">Camera.orthographicSize</a>
/// <a href="http://docs.unity3d.com/ScriptReference/Camera-aspect.html">Camera.aspect</a>
[RequireComponent(typeof(Camera))]
public class InfinityCamera : MonoBehaviour {
	
	/// <summary>The seemingly infinite grid we want to resize dynamically during gameplay.</summary>y
	public GFGrid _myGrid;
	
	/// <summary>how much buffer do we want to use?</summary>
	/// More buffer means more to render each time, but less frequent recalculations.
	public float _buffer = 10.0f;
	
	/// <summary>Speed of the camera.</summary>
	public float _cameraSpeed = 5.0f;
	
	/// <summary>Boost factor.</summary>
	public float _speedBoost =  2.0f;

	/// <summary>Boost key to hold down.</summary>
	public string _boostButton = "left shift";
	
	// use these internally to decide when to recalculate
	private Transform _transform;
	private Vector3 _lastPosition;
	private Camera _cam;
	
	void Awake () {
		// we will be referencing the transform every frame, so better cache it
		_transform = transform;
		_lastPosition = _transform.position;
		
		// the same applies to the camera
		_cam = GetComponent<Camera>();
		_cam.orthographic = true; // make sure it is orthographic (we will use an orthographic camera here because it is simpler)
		
		// set the grid's range according to the camera's size plus the buffer we want to use
		float x = _cam.aspect * _cam.orthographicSize + 1.1f * _buffer; // I scale the buffer with 1.1 to make it a little bit larger, just in case
		float y = _cam.orthographicSize + 1.1f * _buffer;
		_myGrid.useCustomRenderRange = true;
		_myGrid.renderFrom = new Vector3(_transform.position.x - x, _transform.position.y - y, 0); // we set the range relative to the camera's position
		_myGrid.renderTo = new Vector3(_transform.position.x + x, _transform.position.y + y, 0);
	}
	
	void Update () {
		HandleMovement(); // first move the grid
		
		// if we exceeded the buffer limit let's recalculate the points for rendering
		if (Mathf.Abs(_transform.position.x - _lastPosition.x) >= _buffer || Mathf.Abs(_transform.position.y - _lastPosition.y) >= _buffer)
			ResizeGrid();
	}
	
	void ResizeGrid () {
		Vector3 rangeShift = Vector3.zero; // we can't manipulate the individual components of renderFrom/To directly so use a temp variable
		for (int i = 0; i < 2; i++) {
			rangeShift[i] += _transform.position[i] - _lastPosition[i]; // use the difference in positions for the shift
		}
		
		_myGrid.renderFrom += rangeShift; // now add the shift to both values
		_myGrid.renderTo   += rangeShift;
		
		_lastPosition = _transform.position; // save this current position as the new last fixed point
	}
	
	// just a simple method for handling movement using the arrow keys (hold boostButton to move faster)
	void HandleMovement(){
		_transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _cameraSpeed * (Input.GetKey(_boostButton) ? _speedBoost : 1 ) * Time.deltaTime;
	}
	
	// display some information
	void OnGUI () {
		GUI.TextArea(new Rect (10, 10, 600, 150), "Use arrow keys to scroll the camera, hold shift to scroll faster. " +
			"The current camera position is\n\t" + _transform.position.x +" / "+_transform.position.y + "\n and the last fixed position was\n\t" + _lastPosition.x +" / "+ _lastPosition.y +
			"\n\vThe grid's rendering range is only adjusted when the camera reaches the edge of the grid, and that prompts the grid to re-calculate its points. This gives us the illusion" +
			" of a seemingly endless grid while we only render what's within close reach." +
			"This is cheaper performance-wise than rendering a huge grid when the player will only see a small part of it at any given time.");
	}
}

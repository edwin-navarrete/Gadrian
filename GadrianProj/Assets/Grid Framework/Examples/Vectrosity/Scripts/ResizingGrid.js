#pragma strict
// UNCOMMENT THIS SCRIPT FOR IT TO WORK
//Make a resizing grid and render it using Vectrosity. In reality you shouldn't call Resize every frame
// for performance reasons.

public var lineWidth: float = 7.0;
public var lineMaterial: Material;

//private var grid: GFRectGrid;
private var grid: GFHexGrid;
private var cachedTransform: Transform;
private var tempPos: Vector3;

//some toggling variables for resizing
//private var growingSpacingX = true;
//private var growingSpacingY = true;
private var growingRadius = true;
private var growingSizeX = true;
private var growingSizeY = true;

/*
private var gridLine: Vectrosity.VectorLine;

function Start () {
	cachedTransform = transform;
	// in order for the rendering to align properly with the grid the grid has to be at the world's origin
	tempPos = cachedTransform.position;
	cachedTransform.position = Vector3.zero;
	grid = GetComponent.<GFHexGrid>();
	
	if(lineWidth < 1.0) lineWidth = 1.0;
	
	gridLine = new Vectrosity.VectorLine("Resizing Lines", grid.GetVectrosityPoints(), Color.yellow, lineMaterial, lineWidth);
	gridLine.drawTransform = cachedTransform;
	gridLine.Draw3DAuto();
	
	cachedTransform.position = tempPos;
}

function Update () {
	resizeGrid();
	
	// in order for the rendering to align properly with the grid the grid has to be at the world's origin
	tempPos = cachedTransform.position;
	cachedTransform.position = Vector3.zero;
	gridLine.Resize(grid.GetVectrosityPoints()); //calculate the new grid points
	cachedTransform.position = tempPos;
}

function resizeGrid(){
	//if(growingSpacingX){
	//	grid.spacing.x = Mathf.MoveTowards(grid.spacing.x, 3.0, Random.Range(0.25, 0.5)*Time.deltaTime);
	//	if(Mathf.Abs(grid.spacing.x - 3.0) < 0.01)
	//		growingSpacingX = false;
	//} else{
	//	grid.spacing.x = Mathf.MoveTowards(grid.spacing.x, 2.0, Random.Range(0.25, 0.5)*Time.deltaTime);
	//	if(Mathf.Abs(grid.spacing.x - 2.0) < 0.01)
	//		growingSpacingX = true;
	//}
	if(growingRadius) {
		grid.radius = Mathf.MoveTowards(grid.radius, 3.0, Random.Range(0.25, 0.5)*Time.deltaTime);
		if(Mathf.Abs(grid.radius - 3.0) < 0.01)
			growingRadius = false;
	} else {
		grid.radius = Mathf.MoveTowards(grid.radius, 2.0, Random.Range(0.25, 0.5)*Time.deltaTime);
		if(Mathf.Abs(grid.radius - 2.0) < 0.01)
			growingRadius = true;
	}
	
	if(growingSizeX){
		grid.size.x = Mathf.MoveTowards(grid.size.x, 8.0, Random.Range(2.0, 3.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.x - 8.0) < 0.03)
			growingSizeX = false;
	} else{
		grid.size.x = Mathf.MoveTowards(grid.size.x, 3.0, Random.Range(1.0, 3.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.x - 3.0) < 0.01)
			growingSizeX = true;
	}
	
	//if(growingSpacingY){
	//	grid.spacing.y = Mathf.MoveTowards(grid.spacing.y, 2.0, Random.Range(0.25, 0.5)*Time.deltaTime);
	//	if(Mathf.Abs(grid.spacing.y - 2.0) < 0.01)
	//		growingSpacingY = false;
	//} else{
	//	grid.spacing.y = Mathf.MoveTowards(grid.spacing.y, 1.0, Random.Range(0.25, 0.5)*Time.deltaTime);
	//	if(Mathf.Abs(grid.spacing.y - 1.0) < 0.01)
	//		growingSpacingY = true;
	//}
	
	if(growingSizeY){
		grid.size.y = Mathf.MoveTowards(grid.size.y, 4.0, Random.Range(1.0, 2.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.y - 4.0) < 0.03)
			growingSizeY = false;
	} else{
		grid.size.y = Mathf.MoveTowards(grid.size.y, 2.0, Random.Range(1.0, 2.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.y - 2.0) < 0.01)
			growingSizeY = true;
	}
}
*/

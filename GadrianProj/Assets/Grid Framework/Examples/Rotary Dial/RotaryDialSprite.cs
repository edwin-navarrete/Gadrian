using UnityEngine;

// this is a simple script to generate a square mesh to hold the (sprite) image of the dial

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(GFPolarGrid))]
public class RotaryDialSprite : MonoBehaviour {

	void Awake () {
		float size = GetComponent<GFPolarGrid> ().size.x; // this the radius of the grid drawing
		Mesh mesh = BuildMesh (size);
		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	/// <summary>Builds a simple two-triangle mesh.</summary>
	Mesh BuildMesh (float size = 1.0f) {
		var mesh = new Mesh ();
		mesh.vertices = new [] {
			new Vector3 (-size, -size, 0), // bottom left
			new Vector3 (-size,  size, 0), // top left
			new Vector3 ( size,  size, 0), // top right 
			new Vector3 ( size, -size, 0)  // bottom right
		};
		mesh.triangles = new [] { 0, 1, 2, 0, 2, 3 };
		mesh.uv = new [] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)};

		mesh.Optimize();
		return mesh;
	}
}

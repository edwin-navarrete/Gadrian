using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent ( typeof ( Collider2D ) )]
public class SnapCharacter : MonoBehaviour
{
	[SerializeField]
	private GFHexGrid grid;
	private Collider gridCollider;

	private bool beingDragged;
	private Vector3 oldPosition;
	private int intersecting;

	private new SpriteRenderer renderer;

	private void Awake ()
	{
		grid = GameObject.FindGameObjectWithTag ( "Grid" ).GetComponent<GFHexGrid> ();

		if ( grid != null )
		{
			gridCollider = grid.GetComponent<Collider> ();
		}

		grid.AlignTransform ( this.transform );
		renderer = GetComponent<SpriteRenderer> ();
		oldPosition = this.transform.position;

		SetupRigidbody ();
	}

	private void SetupRigidbody ()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		if ( rb == null )
			rb = gameObject.AddComponent<Rigidbody2D> ();

		rb.isKinematic = false;
		rb.gravityScale = 0.0f;
	}

	private void OnMouseDown ()
	{
		beingDragged = true;
		renderer.sortingOrder = 1;
	}

	private void OnMouseUp ()
	{
		beingDragged = false;
		transform.position = oldPosition;
		renderer.sortingOrder = 0;
		intersecting = 0;
		grid.AlignTransform ( this.transform );
		TintRed ( intersecting );
	}

	private void FixedUpdate ()
	{
		if ( beingDragged )
		{
			if ( intersecting == 0 )
			{
				oldPosition = transform.position;
			}
			DragObject ();
		}
	}

	private void DragObject ()
	{
		if ( !grid || !gridCollider )
			return;

		Vector3 cursorWorldPoint = ShootRay ();

		this.transform.position = cursorWorldPoint;
	}

	private Vector3 ShootRay ()
	{
		RaycastHit hit;
		gridCollider.Raycast ( Camera.main.ScreenPointToRay ( Input.mousePosition ), out hit, Mathf.Infinity );
		return hit.collider != null ? hit.point : transform.position;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		SetIntersecting ( true );
	}

	private void OnTriggerExit2D (Collider2D other)
	{
		SetIntersecting ( false );
	}

	public void SetIntersecting (bool intersecting)
	{
		if ( !beingDragged ) // ignore sitting objects, only moving ones should respond
			return;
		// if true we entered another object, increment the value; if false we exited another object, decrease the value
		this.intersecting = intersecting ? this.intersecting + 1 : this.intersecting - 1;
		//update the colour
		TintRed ( this.intersecting );
	}

	void TintRed (int intersections)
	{
		if ( intersections > 0 )
		{
			renderer.material.color = Color.red;
		}
		else
		{
			renderer.material.color = Color.white;
		}
	}
}

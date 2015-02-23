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
	private Vector3 lastValidPosition;
	private Vector3 oldPosition;
	private int intersecting;

	private new SpriteRenderer renderer;

	public event UnityEngine.Events.UnityAction Movement;

	private void OnMovement ()
	{
		if ( Movement.GetInvocationList ().Length != 0 )
		{
			Movement ();
		}
	}

	private void Awake ()
	{
		grid = GameObject.FindGameObjectWithTag ( "Grid" ).GetComponent<GFHexGrid> ();

		if ( grid != null )
		{
			gridCollider = grid.GetComponent<Collider> ();
		}

		grid.AlignTransform ( this.transform );
		renderer = GetComponent<SpriteRenderer> ();
		lastValidPosition = this.transform.position;
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
		
		if ( CheckIfMovement () )
		{
			transform.position = lastValidPosition;
			oldPosition = transform.position;
		}
		renderer.sortingOrder = 0;
		intersecting = 0;
		grid.AlignTransform ( this.transform );
		TintRed ( intersecting );
		OnMovement ();
	}

	private bool CheckIfMovement ()
	{
		Vector3 newPosition = grid.WorldToGrid ( this.transform.position );
		float distance = Vector3.Distance ( newPosition, grid.WorldToGrid ( oldPosition ) );
		return distance > 0.5f;
	}
	private void FixedUpdate ()
	{
		if ( beingDragged )
		{
			if ( intersecting == 0 )
			{
				lastValidPosition = transform.position;
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
		if ( other.transform.tag == "Player" )
		{
			SetIntersecting ( true );
		}
	}

	private void OnTriggerExit2D (Collider2D other)
	{
		if ( other.transform.tag == "Player" )
		{
			SetIntersecting ( false );
		}
	}

	private void SetIntersecting (bool intersecting)
	{
		if ( !beingDragged ) // ignore sitting objects, only moving ones should respond
			return;
		// if true we entered another object, increment the value; if false we exited another object, decrease the value
		this.intersecting = intersecting ? this.intersecting + 1 : this.intersecting - 1;
		//update the colour
		TintRed ( this.intersecting );
	}

	private void TintRed (int intersections)
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

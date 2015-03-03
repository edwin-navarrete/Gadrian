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

	public event UnityEngine.Events.UnityAction<bool> Intersecting;

	public static event UnityEngine.Events.UnityAction MovingCharacter;
	public static event UnityEngine.Events.UnityAction MovedCharacter;


	private void OnIntersecting (bool intersecting)
	{
		if ( Intersecting != null )
		{
			Intersecting ( intersecting );
		}
	}

	private void OnMovingCharacter ()
	{
		if ( MovingCharacter != null )
		{
			MovingCharacter ();
		}
	}

	private void OnMovedCharacter ()
	{
		if ( MovedCharacter != null )
		{
			MovedCharacter ();
		}
	}

	private void OnMovement ()
	{
		CharacterManager.Instance.RefreshMoods();		
	}

	private void Awake ()
	{
		grid = GameObject.FindGameObjectWithTag ( "Grid" ).GetComponent<GFHexGrid> ();

		if ( grid != null )
		{
			gridCollider = grid.GetComponent<Collider> ();
		}

		grid.AlignTransform ( this.transform );
		//backgroundRenderer = GetComponentInChildren<SpriteRenderer> ();
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
		OnMovingCharacter ();
		beingDragged = true;
	}

	private void OnMouseUp ()
	{
		OnMovedCharacter ();
		beingDragged = false;
		
		if ( CheckIfMovement () )
		{
			transform.position = lastValidPosition;
			oldPosition = transform.position;
		}
		intersecting = 0;
		grid.AlignTransform ( this.transform );
		OnIntersecting ( false );
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
		if ( !grid )
			return;

		Vector3 cursorWorldPoint = ShootRay ();

		this.transform.position = cursorWorldPoint;
	}

	private Vector3 ShootRay ()
	{
		LayerMask gridLayer = 1 << LayerMask.NameToLayer ( "Grid" );
		Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		Vector2 orgin = new Vector2 ( ray.origin.x, ray.origin.y );

		RaycastHit2D hit = Physics2D.Raycast ( orgin, Vector2.zero, float.PositiveInfinity, gridLayer );
		return hit.collider != null ? (Vector3) hit.point : transform.position;
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

		if ( this.intersecting > 0 )
		{
			OnIntersecting ( true );
		}
		else
		{
			OnIntersecting ( false );
		}
	}
}

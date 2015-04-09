using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent( typeof( Collider2D ) )]
public class SnapCharacter : MonoBehaviour
{
    [SerializeField]
    private GFHexGrid grid;

    private int intersecting;
    private bool beingDragged;

    private Vector3 lastValidPosition;
    private Vector3 oldPosition;
    private PlayerOverTile lastTile;

    public event UnityEngine.Events.UnityAction<bool> Intersecting;

    public static event UnityEngine.Events.UnityAction MovingCharacter;
    public static event UnityEngine.Events.UnityAction MovedCharacter;


    private void OnIntersecting (bool intersecting)
    {
        if ( Intersecting != null )
        {
            Intersecting( intersecting );
        }
    }

    private void OnMovingCharacter ()
    {
        if ( MovingCharacter != null )
        {
            MovingCharacter();
        }
    }

    private void OnMovedCharacter ()
    {
        if ( MovedCharacter != null )
        {
            MovedCharacter();
        }
    }

    private void Awake ()
    {
        grid = GridManager.Instance.Grid as GFHexGrid;
        grid.AlignTransform( this.transform );
        lastValidPosition = this.transform.position;
        oldPosition = this.transform.position;

        SetupRigidbody();
    }

    public void Start ()
    {
        CharacterManager.Instance.Winning += MakeNonBlocking;
    }

    public void InitializeCharacterPosition (Vector3 firstPosition, PlayerOverTile firstTile)
    {
        transform.position = firstPosition;
        grid.AlignTransform( transform );

        lastTile = firstTile;
        lastTile.SolidifyTile();
    }

    private void MakeNonBlocking ()
    {
        //Debug.Log ( "Character non-blocking layer" );
        gameObject.layer = 2;
    }

    private void SetupRigidbody ()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if ( rb == null )
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.isKinematic = false;
        rb.gravityScale = 0.0f;
    }

    private void OnMouseDown ()
    {
        OnMovingCharacter();
        lastTile.UnsolidifyTile();
        beingDragged = true;
    }

    private void OnMouseUp ()
    {
        beingDragged = false;

        if ( CheckIfMovement() )
        {
            DoMovement( lastValidPosition, true );
            OnMovedCharacter();
        }
        else
        {
            transform.position = oldPosition;
        }
        intersecting = 0;
        OnIntersecting( false );
    }

    public void DoMovement (Vector3 newPosition, bool registerMovement)
    {
        transform.position = newPosition;
        grid.AlignTransform( this.transform );
        CheckTileToSolidify();
        if ( registerMovement )
        {
            Movement movement = new Movement( gameObject, MovementAction.Movement, oldPosition, transform.position );
            CharacterManager.Instance.RegisterMovement( movement );
        }
        oldPosition = transform.position;
    }

    private void CheckTileToSolidify ()
    {
        LayerMask gridLayer = 1 << LayerMask.NameToLayer( "Grid" );
        Ray ray = Camera.main.ScreenPointToRay( transform.position );
        Vector2 orgin = new Vector2( ray.origin.x, ray.origin.y );

        RaycastHit2D hit = Physics2D.Raycast( orgin, Vector2.zero, float.PositiveInfinity, gridLayer );
        if ( hit.collider != null )
        {
            if ( hit.collider.tag == "Cell" )
            {
                hit.transform.SendMessage( "SolidifyTile" );
                lastTile = hit.transform.GetComponent<PlayerOverTile>();
            }
        }
    }

    private bool CheckIfMovement ()
    {
        Vector3 newPosition = grid.WorldToGrid( this.transform.position );
        float distance = Vector3.Distance( newPosition, grid.WorldToGrid( oldPosition ) );
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
            DragObject();
        }
    }

    private void DragObject ()
    {
        if ( !grid )
            return;

        Vector3 cursorWorldPoint = ShootRay();

        this.transform.position = cursorWorldPoint;
    }

    private Vector3 ShootRay ()
    {
        LayerMask gridLayer = 1 << LayerMask.NameToLayer( "Grid" );
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        Vector2 orgin = new Vector2( ray.origin.x, ray.origin.y );

        RaycastHit2D hit = Physics2D.Raycast( orgin, Vector2.zero, float.PositiveInfinity, gridLayer );
        if ( hit.collider != null )
        {
            if ( hit.collider.tag == "Cell" )
            {
                return (Vector3) hit.point;
            }
        }
        return transform.position;

    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if ( other.transform.tag == "Player" )
        {
            SetIntersecting( true );
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if ( other.transform.tag == "Player" )
        {
            SetIntersecting( false );
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
            OnIntersecting( true );
        }
        else
        {
            OnIntersecting( false );
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent( typeof( Collider2D ) )]
public class SnapCharacter : MonoBehaviour
{
    [SerializeField]
    private GFGrid grid;

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

    private void OnMovement ()
    {
        CharacterManager.Instance.RefreshMoods();
    }

    private void Awake ()
    {
        grid = GridManager.Instance.Grid;

        grid.AlignTransform( this.transform );
        lastValidPosition = this.transform.position;
        oldPosition = this.transform.position;

        SetupRigidbody();
    }

    public void Start ()
    {
        CharacterManager.Instance.Winning += MakeNonBlocking;
    }

    public void InitializeCharacter (Vector3 firstPosition, PlayerOverTile firstTile)
    {
        transform.position = firstPosition;
        grid.AlignTransformFixed( transform );

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
        }
        else
        {
            transform.position = oldPosition;
        }
        intersecting = 0;
        OnIntersecting( false );
        OnMovement();
    }

    public void DoMovement (Vector3 newPosition, bool registerMovement)
    {
        OnMovedCharacter();

        transform.position = newPosition;
        grid.AlignTransformFixed( this.transform );
        CheckTileToSolidify();
        if ( registerMovement )
        {
            Movement movement = new Movement( gameObject, Action.Movement, oldPosition, transform.position );
            CharacterManager.Instance.RegisterMovement( movement );
        }
        oldPosition = transform.position;
    }

    private void CheckTileToSolidify ()
    {
        LayerMask gridLayer = 1 << LayerMask.NameToLayer( "Grid" );
        Debug.LogFormat( "GridLayer: {0}, Mouse positio: {1}", gridLayer.value, Input.mousePosition );

        Ray2D ray = new Ray2D( new Vector2( transform.position.x, transform.position.y ), Vector2.zero );
        RaycastHit2D hit = Physics2D.Raycast( ray.origin, Vector2.zero, float.PositiveInfinity, gridLayer );
        if ( hit.collider != null )
        {
            if ( hit.collider.tag == "Cell" )
            {
                Debug.Log( "Hit a tile" );
                PlayerOverTile tile = hit.transform.GetComponent<PlayerOverTile>();
                tile.SolidifyTile();
                lastTile = tile;
            }
        }
    }

    private bool CheckIfMovement ()
    {
        Vector3 newPosition = grid.WorldToGridFixed( this.transform.position );
        float distance = Vector3.Distance( newPosition, grid.WorldToGridFixed( oldPosition ) );
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
            if ( hit.transform.tag == "Cell" )
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

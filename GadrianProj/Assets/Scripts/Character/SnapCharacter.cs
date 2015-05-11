using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent( typeof( Collider2D ) )]
public class SnapCharacter : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GFGrid grid;

    private Vector3 oldPosition;
    private PlayerOverTile lastTile;

    #endregion

    #region Events

    public static event UnityEngine.Events.UnityAction MovingCharacter;
    public static event UnityEngine.Events.UnityAction MovedCharacter;

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

    #endregion

    #region Initialization

    private void Awake ()
    {
        grid = GridManager.Instance.Grid;

        grid.AlignTransform( transform );
        oldPosition = transform.position;

        SetupRigidbody();
    }

    public void Start ()
    {
        CharacterManager.Instance.Winning += MakeNonBlocking;
    }

    private void MakeNonBlocking ()
    {
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

    #endregion

    #region Mouse Callbacks

    private void OnMouseDown ()
    {
        OnMovingCharacter();
        //UnsolidifyLastTile();
    }
    
    private void OnMouseUp ()
    {
        if ( CheckIfMovement() )
        {
            grid.AlignTransformFixed( transform );
            DoMovement( transform.position, true );
            OnMovedCharacter();
            CharacterManager.Instance.RefreshMoods();
        }
        else
        {
            transform.position = oldPosition;
        }
    }

    private bool CheckIfMovement ()
    {
        Vector3 newPosition = grid.WorldToGridFixed( transform.position );
        float distance = Vector3.Distance( newPosition, grid.WorldToGridFixed( oldPosition ) );
        return distance > 0.5f;
    }

    public void OnMouseDrag ()
    {
        DragObject();
    }

    #endregion

    #region Character Dragging

    private void DragObject ()
    {
        if ( !grid ) return;

        Vector3 cursorWorldPoint = ShootRay();

        this.transform.position = cursorWorldPoint;
    }

    private Vector3 ShootRay ()
    {
        LayerMask gridLayer = LayerMask.GetMask( "Grid" );
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

    #endregion

    #region Movement Handling

    public void DoSwapMovement (Vector3 swapPosition)
    {
        transform.position = swapPosition;
        UnsolidifyLastTile();
        CheckTileToSolidify();
        oldPosition = transform.position;
    }

    public void DoMovement (Vector3 newPosition, bool registerMovement)
    {
        transform.position = newPosition;
        SnapCharacter swapCharacter = CheckCharacterToSwap();
        if ( swapCharacter )
        {
            swapCharacter.DoSwapMovement( oldPosition );
            CheckTileToSolidify();
        }
        else
        {
            UnsolidifyLastTile();
            CheckTileToSolidify();
        }
        if ( registerMovement )
        {
            Movement movement = new Movement( gameObject, oldPosition, transform.position );
            CharacterManager.Instance.RegisterMovement( movement );
        }
        oldPosition = transform.position;
    }

    private SnapCharacter CheckCharacterToSwap ()
    {
        // Make this gameObject ignore raycasting
        gameObject.layer = LayerMask.NameToLayer( "Ignore Raycast" );
        // Get the Characters layer
        LayerMask gridLayer = 1 << LayerMask.NameToLayer( "Players" );

        Ray2D ray = new Ray2D( new Vector2( transform.position.x, transform.position.y ), Vector2.zero );
        RaycastHit2D hit = Physics2D.Raycast( ray.origin, Vector2.zero, float.PositiveInfinity, gridLayer );
        if ( hit.collider != null )
        {
            if ( hit.collider.tag == "Player" )
            {
                gameObject.layer = LayerMask.NameToLayer( "Players" );
                return hit.transform.GetComponent<SnapCharacter>();
            }
        }
        gameObject.layer = LayerMask.NameToLayer( "Players" );
        return null;
    }

    private void UnsolidifyLastTile ()
    {
        if ( lastTile == null ) return;

        if ( lastTile.IsTileSolid() )
        {
            lastTile.UnsolidifyTile();
        }
    }

    private void CheckTileToSolidify ()
    {
        LayerMask gridLayer = 1 << LayerMask.NameToLayer( "Grid" );

        Ray2D ray = new Ray2D( new Vector2( transform.position.x, transform.position.y ), Vector2.zero );
        RaycastHit2D hit = Physics2D.Raycast( ray.origin, Vector2.zero, float.PositiveInfinity, gridLayer );
        if ( hit.collider != null )
        {
            if ( hit.collider.tag == "Cell" )
            {
                PlayerOverTile tile = hit.transform.GetComponent<PlayerOverTile>();
                tile.SolidifyTile();
                lastTile = tile;
            }
        }
    }

    #endregion
}

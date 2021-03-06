﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[RequireComponent( typeof( Collider2D ) )]
public class SnapCharacter : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GFGrid grid;

    [SerializeField]
    private Sprite happyTile;

    [SerializeField]
    private Sprite sadTile;

    [SerializeField]
    private Sprite angryTile;

    [SerializeField]
    private Sprite scaredTile;

    private Vector3 oldPosition;
    private PlayerOverTile lastTile;

    #endregion

    #region Initialization

    private void Awake ()
    {
        grid = GridManager.Instance.Grid;

        grid.AlignTransform( transform );
        oldPosition = transform.position;
        Personality p = this.transform.GetComponent<Personality>();
        if (p != null)
        {
            p.Happy += () => {
                lastTile.MoodTile(happyTile);
            };
            p.Sad += () => {
                lastTile.MoodTile(sadTile);
            };
            p.Scared += () => {
                lastTile.MoodTile(scaredTile);
            };
            p.Angry += () => {
                lastTile.MoodTile(angryTile);
            };
        }
        SetupRigidbody();
    }
    
    private Transform tileAtPos()
    {
        LayerMask gridLayer = 1 << LayerMask.NameToLayer("Grid");
        Debug.Log("gridLayer:" + transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, gridLayer);
        Debug.Log("FOund hit?:" + hit.centroid);
        if (hit.transform != null)
        {
            Debug.Log("FOund tag?:" + hit.collider.tag);
        }

        if (hit.collider != null && hit.transform.tag == "Cell")
        {
            return hit.transform;
        }
        return null;
    }

    public void OnEnable ()
    {
        EventManager.StartListening( Events.Winning, MakeNonBlocking );
        EventManager.StartListening( Events.Loss, MakeNonBlocking );
        EventManager.StartListening( Events.RefreshingPersonalities, MakeNonBlocking );
        EventManager.StartListening( Events.RefreshedPersonalities, MakeBlocking );
    }

    public void OnDisable ()
    {
        EventManager.StopListening( Events.Winning, MakeNonBlocking );
        EventManager.StopListening( Events.Loss, MakeNonBlocking );
        EventManager.StopListening( Events.RefreshingPersonalities, MakeNonBlocking );
        EventManager.StopListening( Events.RefreshedPersonalities, MakeBlocking );
    }

    private void MakeNonBlocking ()
    {
        gameObject.layer = LayerMask.NameToLayer( "Ignore Raycast" );
    }

    private void MakeBlocking ()
    {
        gameObject.layer = LayerMask.NameToLayer( "Players" );
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
        EventManager.TriggerEvent( Events.MovingCharacter );
    }
    
    private void OnMouseUp ()
    {
        if ( CheckIfMovement() )
        {
            grid.AlignTransformFixed( transform );
            DoMovement( transform.position, true );
            EventManager.TriggerEvent( Events.MovedCharacter );
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

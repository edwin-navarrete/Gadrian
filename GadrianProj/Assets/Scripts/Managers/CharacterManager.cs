﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    [Range( 1, 12 )]
    public int characterAmount = 5;
    private List<int> selec;
    private int index;

    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private RectTransform characterRectTransform;
    [SerializeField]
    private GFGrid grid;

    [SerializeField]
    private GameObject characterPrefab;

    private GameObject lastCharSelected;		// Object from the scroll list that was last selected
    private Transform characterPlaceholder;		// Parent in hierarchy to make all characters children of

    private List<Personality> characters;		// Reference to all characters placed in world
    private List<Movement> movements;

    #region Events

    public event UnityEngine.Events.UnityAction<Sprite, Sprite> StartingDrag;
    public event UnityEngine.Events.UnityAction FinishingDrag;
    public event UnityEngine.Events.UnityAction FinishedDrag;
    public event UnityEngine.Events.UnityAction Winning;
    public event UnityEngine.Events.UnityAction Won;
    public event UnityEngine.Events.UnityAction FinishingCharacterPlacement;


    private void OnStartingDrag (Sprite body, Sprite complexion)
    {
        if ( StartingDrag != null )
        {
            StartingDrag( body, complexion );
        }
    }

    private void OnFinishingDrag ()
    {
        if ( FinishingDrag != null )
        {
            FinishingDrag();
        }
    }

    private void OnFinishedDrag ()
    {
        if ( FinishedDrag != null )
        {
            FinishedDrag();
        }
    }

    private void OnWinning ()
    {
        if ( Winning != null )
        {
            Winning();
        }
    }

    private void OnWon ()
    {
        if ( Won != null )
        {
            Won();
        }
    }

    private void OnFinishingCharacterPlacement ()
    {
        if ( FinishingCharacterPlacement != null )
        {
            FinishingCharacterPlacement();
        }
    }

    #endregion

    #region Singleton

    private static CharacterManager instance;

    public static CharacterManager Instance
    {
        get
        {
            if ( instance == null )
            {
                instance = GameObject.FindObjectOfType<CharacterManager>();

                if ( instance == null )
                {
                    GameObject newGO = new GameObject( "Character manager" );
                    instance = newGO.AddComponent<CharacterManager>();
                }

                DontDestroyOnLoad( instance );
            }

            return instance;
        }
    }

    #endregion

    // This awake method could be remove if the character manager already have reference in Editor to canvasRectTransform,
    // characterRectTransform and CharacterPlaceholder
    public void Awake ()
    {
        //canvasRectTransform = GameObject.FindObjectOfType<Canvas>().transform as RectTransform;
        //characterRectTransform = GameObject.FindObjectOfType<CharacterRepresentation>().transform as RectTransform;
        characterPlaceholder = GameObject.FindGameObjectWithTag( "Placeholder" ).transform;
        grid = GridManager.Instance.Grid;
        characters = new List<Personality>();
        movements = new List<Movement>();
    }

    public void OnEnable ()
    {
        EventManager.StartListening( "LevelGenerated", PopulateLevel );
    }

    public void OnDisable ()
    {
        EventManager.StopListening( "LevelGenerated", PopulateLevel );
    }

    public void Start ()
    {
        if ( CharacterManager.Instance != this )
            Destroy( this.gameObject );
    }

    public Vector3 AskGridPosition (Vector3 position)
    {
        return grid.WorldToGrid( position );
    }

    #region Mood refresh

    public void RefreshMoods ()
    {
        foreach ( Personality personality in characters )
        {
            List<Personality> neighb = GetNeighbourPersonalities( personality.transform.position );
            personality.RefreshMood( neighb );
        }
    }

    private List<Personality> GetNeighbourPersonalities (Vector3 curPos)
    {
        List<Personality> neighbourPersonalities = new List<Personality>();
        Vector3 position = grid.WorldToGrid( curPos );
        foreach ( Personality personality in characters )
        {
            Vector3 reference = grid.WorldToGrid( personality.transform.position );
            if ( reference == position )
                continue;

            bool isNeighbour = false;
            if ( Mathf.Abs( reference.x - position.x ) < 1.1f && Mathf.Abs( reference.y - position.y ) < 0.1f )
            {//straight above or below
                isNeighbour = true;
            }
            else
            {
                if ( Mathf.RoundToInt( reference.y ) % 2 == 0 )
                {//two cases, depending on whether the x-coordinate is even or odd
                    //neighbours are either strictly left or right of the switch or right/left and one unit below
                    if ( Mathf.Abs( reference.y - position.y ) < 1.1f && position.x - reference.x < 0.1f && position.x - reference.x > -1.1f )
                        isNeighbour = true;
                }
                else
                {//x-coordinate odd
                    //neighbours are either strictly left or right of the switch or right/left and one unit above
                    if ( Mathf.Abs( reference.y - position.y ) < 1.1f && reference.x - position.x < 0.1f && position.x - reference.x < 1.1f )
                        isNeighbour = true;
                }
            }
            if ( isNeighbour )
                neighbourPersonalities.Add( personality );
        }
        return neighbourPersonalities;
    }

    #endregion

    #region Win and end-game notifications

    private System.Collections.IEnumerator CheckForWin ()
    {
        bool isOver = false;
        while ( !isOver )
        {
            int happyAmount = 0;

            foreach ( Personality personality in characters )
            {
                //Debug.Log( string.Format( "{0} mood is: {1}", personality.gameObject, personality.CurrentMood ) );
                if ( personality.CurrentMood.getFeel() == Mood.HAPPY.getFeel() )
                {
                    happyAmount++;
                }
            }
            if ( happyAmount == characters.Count )
            {
                OnWinning();
                Invoke( "OnWon", 2.0f );
                isOver = true;
                yield break;
            }
            happyAmount = 0;

            yield return null;
        }
    }

    public void FinishCharacterPlacement ()
    {
        OnFinishingCharacterPlacement();
        StartCoroutine( CheckForWin() );
    }

    #endregion

    #region Movement registration

    public void RegisterMovement (Movement newMovement)
    {
        movements.Add( newMovement );
    }

    public void UndoLastMomevent ()
    {
        int index = movements.Count - 1;
        if ( index <= 0 )
        {
            Movement movement = movements[index];
            if ( movement.ActionPerformed == Action.Movement )
            {
                SnapCharacter snapCharacter = movement.Sender.GetComponent<SnapCharacter>();
                snapCharacter.DoMovement( movement.OldPosition, false );
            }
            else if ( movement.ActionPerformed == Action.Placement )
            {
                Destroy( movement.Sender );
            }
            movements.Remove( movement );
        }
        else
        {
            Debug.Log( "There is no more moves" );
        }
    }

    #endregion

    #region Populate Level with Characters

    private void PopulateLevel ()
    {
        selec = new List<int>();
        selec.Add( 1 );
        selec.Add( 2 );
        selec.Add( 5 );
        selec.Add( 6 );
        selec.Add( 8 );

        EventManager.TriggerEvent( "StartingCharacterCreation" );
        for ( int i = 0; i < characterAmount; i++ )
        {
            CreateCharacterItem( null );
            index++;
        }
        EventManager.TriggerEvent( "FinishedCharacterCreating" );
        RefreshMoods();
    }

    public void CreateCharacterItem (Personality personality)
    {
        GameObject newChar = Instantiate( characterPrefab ) as GameObject;

        Character character = newChar.GetComponent<Character>();
        characters.Add( character.personality );

        if ( personality == null )
        {
            character.personality.SetupPersonality( PersonalityManager.PersonalityModel, selec[index] );
        }
        else
        {
            character.personality.CopyPersonality( personality );
        }

        // Modify Character
        character.personality.TraitsEffect();
        // Do movement to the first location
        character.snapCharacter.DoMovement( TileManager.Instance.GetFreeTilePosition(), false );
        // Child to Character holder
        newChar.transform.SetParent( characterPlaceholder );
    }

    #endregion

    #region Character movement from scroll list to world

    /// <summary>
    /// When input from touch/mouse is down, this method will turn off the element of the scroll list 
    /// and then create a object to drag around and finally place on the grid.
    /// </summary>
    /// <param name="characterBody"></param>
    public void SetCharacterImage (Sprite characterBody, Sprite characterComplexion, PointerEventData eventData)
    {
        OnStartingDrag( characterBody, characterComplexion );

        characterRectTransform.position = eventData.position;
    }

    public void MoveCharacterImage (PointerEventData eventData)
    {
        if ( characterRectTransform == null )
            return;

        Vector2 pointerPosition = ClampToWindow( Input.mousePosition );

        Vector2 localPointerPosition;
        if ( RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, pointerPosition, eventData.pressEventCamera, out localPointerPosition ) )
        {
            characterRectTransform.localPosition = localPointerPosition;
        }
    }

    public void PlaceCharacterImage (PointerEventData eventData, GameObject sender, Personality personality, string charName)
    {
        OnFinishingDrag();

        //LayerMask gridLayer = 1 << LayerMask.NameToLayer( "Grid" );
        LayerMask gridLayer = LayerMask.GetMask( "Grid" );
        Debug.LogFormat( "GridLayer: {0}, Mouse position: {1}", gridLayer.value, Input.mousePosition );
        Debug.LogFormat( "GridLayer: {0}, Event data position: {1}", gridLayer.value, eventData.position );

        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        //Vector2 origin = new Vector2( ray.origin.x, ray.origin.y );
        //Ray2D ray = new Ray2D( eventData.position, Vector2.zero );
        //Vector2 origin = new Vector2( ray.origin.x, ray.origin.y );
        RaycastHit2D hit = Physics2D.Raycast( ray.origin, Vector2.zero, float.PositiveInfinity, gridLayer );

        if ( hit.collider != null )
        {
            Debug.Log( "Hit a tile" );
            if ( hit.transform.tag == "Cell" )
            {
                GameObject newCharacter = Instantiate( characterPrefab ) as GameObject;

                Personality newCharPersonality = newCharacter.GetComponent<Personality>();
                characters.Add( newCharPersonality );

                newCharPersonality.CopyPersonality( personality );
                newCharPersonality.TraitsEffect();
                RefreshMoods();

                newCharacter.transform.SetParent( characterPlaceholder );
                sender.SetActive( false );

                SnapCharacter snapCharacter = newCharacter.GetComponent<SnapCharacter>();
                snapCharacter.InitializeCharacter( (Vector3) hit.point, hit.transform.GetComponent<PlayerOverTile>() );
            }
        }

        OnFinishedDrag();
    }

    private Vector2 ClampToWindow (Vector3 mousePoistion)
    {
        Vector2 rawPointerPosition = mousePoistion;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners( canvasCorners );

        float clampedX = Mathf.Clamp( rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x );
        float clampedY = Mathf.Clamp( rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y );

        Vector2 newPointerPosition = new Vector2( clampedX, clampedY );
        return newPointerPosition;
    }

    #endregion
}
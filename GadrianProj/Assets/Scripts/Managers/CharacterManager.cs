using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    #region Fields
    
    private GFGrid grid;

    [SerializeField]
    private GameObject characterPrefab;

    private GameObject lastCharSelected;		// Object from the scroll list that was last selected
    private Transform characterPlaceholder;		// Parent in hierarchy to make all characters children of

    private List<Personality> characters;		// Reference to all characters placed in world
    private List<Movement> movements;

    #endregion

    #region Events

    public event UnityEngine.Events.UnityAction Winning;
    public event UnityEngine.Events.UnityAction Won;
    public event UnityEngine.Events.UnityAction FinishingCharacterPlacement;

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

    #region Unity API

    // This awake method could be remove if the character manager already have reference in Editor to canvasRectTransform,
    // characterRectTransform and CharacterPlaceholder
    public void Awake ()
    {
        characterPlaceholder = GameObject.FindGameObjectWithTag( "Placeholder" ).transform;
        grid = GridManager.Instance.Grid;
        characters = new List<Personality>();
        movements = new List<Movement>();
    }

    public void OnEnable ()
    {
        EventManager.StartListening( Events.LevelGenerated, PopulateLevel );
    }

    public void OnDisable ()
    {
        EventManager.StopListening( Events.LevelGenerated, PopulateLevel );
    }

    public void Start ()
    {
        if ( CharacterManager.Instance != this )
            Destroy( this.gameObject );
    }

    #endregion

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
        Vector3 position = grid.WorldToGridFixed( curPos );
        foreach ( Personality personality in characters )
        {
            Vector3 reference = grid.WorldToGridFixed( personality.transform.position );
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
        if ( index >= 0 )
        {
            Movement movement = movements[index];
            SnapCharacter snapCharacter = movement.Sender.GetComponent<SnapCharacter>();
            snapCharacter.DoMovement( movement.OldPosition, false );
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
        EventManager.TriggerEvent( Events.StartingCharacterCreation );

        TileConfiguration tileConf;
        while ( (tileConf = TileManager.Instance.GetNextCharacterTile()) != null  )
        {
            CreateCharacterItem( tileConf );
        }

        EventManager.TriggerEvent( Events.FinishedCharacterCreating );
        FinishCharacterPlacement();
        RefreshMoods();
    }

    /// <summary>
    /// Create a personlaity based on a pesonality or if the personality is null pick a new personality
    /// </summary>
    /// <param name="personality"></param>
    public void CreateCharacterItem (TileConfiguration tileConfiguration)
    {
        // Instantiate a character prefab
        GameObject newChar = Instantiate( characterPrefab ) as GameObject;
        // Get Character Component
        Character character = newChar.GetComponent<Character>();
        // Add the Character component to the characters list
        characters.Add( character.personality );

        // If the method was call with a null personality pick a new personality
        character.personality.SetupPersonality( PersonalityManager.PersonalityModel, tileConfiguration.personalityIndex );

        // Modify Character
        character.personality.TraitsEffect();
        // Do the first movement to a free tile position
        character.snapCharacter.DoMovement( GridManager.Instance.Grid.GridToWorldFixed( tileConfiguration.position ), false );
        // Child to Character holder
        newChar.transform.SetParent( characterPlaceholder );
    }

    #endregion
}

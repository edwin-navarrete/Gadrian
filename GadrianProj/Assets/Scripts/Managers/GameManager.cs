using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    private CharacterManager characterManager;
    private MenuManager menuManager;
    private GridManager gridManager;
    private PersonalityManager personalitymanager;
    private LevelGenerator levelGenerator;

    private GameState state;

    #region Properties

    private CharacterManager CharaterManager
    {
        get
        {
            if ( characterManager == null )
            {
                characterManager = CharacterManager.Instance;
            }
            return characterManager;
        }
    }

    private MenuManager MenuManager
    {
        get
        {
            if ( menuManager == null )
            {
                menuManager = MenuManager.Instance;
            }
            return menuManager;
        }
    }

    private GridManager GridManager
    {
        get
        {
            if ( gridManager == null )
            {
                gridManager = GridManager.Instance;
            }
            return gridManager;
        }
    }

    private PersonalityManager PersonalityManager
    {
        get
        {
            if ( personalitymanager == null )
            {
                personalitymanager = PersonalityManager.Instance;
            }
            return personalitymanager;
        }
    }

    private LevelGenerator LevelGenerator
    {
        get
        {
            if ( levelGenerator == null )
            {
                levelGenerator = LevelGenerator.Instance;
            }
            return levelGenerator;
        }
    }

    public GameState CurrentGameState
    {
        private set
        {
            state.FinalizeState();
            state = value;
            state.InitializeState();
        }

        get
        {
            return state;
        }
    }

    #endregion

    #region Events

    public event UnityEngine.Events.UnityAction<GameState> StateChanging;
    public event UnityEngine.Events.UnityAction<GameState> StateChanged;

    private void OnStateChanging( GameState state )
    {
        if ( StateChanging != null )
        {
            StateChanging( state );
        }
    }

    private void OnStateChanged( GameState state )
    {
        if ( StateChanged != null )
        {
            StateChanged( state );
        }
    }

    #endregion
    
    public void OnLevelWasLoaded( int level )
    {
        if ( level == 0 )
            state = new SplashScreen();
    }

    public void ChangeGameState( GameState newState )
    {
        if ( IsSwitchable(newState) )
        {
            CurrentGameState = newState;
        }
    }

    private bool IsValidAction(GameStateAction actionToPerform)
    {
        return CurrentGameState.actionsToPerform.Contains( actionToPerform );
    }

    private bool IsSwitchable(GameState stateToSwitch)
    {
        return CurrentGameState.switchableStates.Contains( stateToSwitch );
    }
}

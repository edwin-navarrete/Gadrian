using UnityEngine;
using System.Collections;

public class MenuScreen : GameState
{
    public MenuScreen()
    {
        actionsToPerform.Add( GameStateAction.ShowOptions );
        actionsToPerform.Add( GameStateAction.ShowStore );
        actionsToPerform.Add( GameStateAction.StartNewGame );
        actionsToPerform.Add( GameStateAction.StartNextLevel );
    }

    public override void InitializeState()
    {
    }

    public override void FinalizeState()
    {
    }
}

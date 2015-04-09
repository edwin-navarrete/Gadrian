using UnityEngine;
using System.Collections;

public class SplashScreen : GameState
{
    public SplashScreen()
    {
        actionsToPerform.Add( GameStateAction.None );
    }

    public override void InitializeState()
    {
        MenuManager.Instance.LoadingScreen();
    }

    public override void FinalizeState()
    {
        MenuManager.Instance.FadeOutScreen();
    }
}

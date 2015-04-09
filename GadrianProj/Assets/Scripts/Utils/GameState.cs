using UnityEngine;
using System.Collections.Generic;

public abstract class GameState
{
    public readonly List<GameStateAction> actionsToPerform;
    public readonly List<GameState> switchableStates;

    public abstract void InitializeState();

    public abstract void FinalizeState();
}

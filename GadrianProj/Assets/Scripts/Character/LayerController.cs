using UnityEngine;
using System.Collections;

/// <summary>
/// This Script is used on Tiles, to make tiles switch between layers when the characters are drag
/// </summary>
public class LayerController : MonoBehaviour
{
    public void OnEnable ()
    {
        EventManager.StartListening( Events.MovingCharacter, SetLayerToGrid );
        EventManager.StartListening( Events.MovedCharacter, SetLayerToIgnore );

        EventManager.StartListening( Events.StartingCharacterCreation, SetLayerToGrid );
        EventManager.StartListening( Events.FinishedCharacterCreating, SetLayerToIgnore );
    }

    public void OnDisable ()
    {
        EventManager.StopListening( Events.MovingCharacter, SetLayerToGrid );
        EventManager.StopListening( Events.MovedCharacter, SetLayerToIgnore );

        EventManager.StopListening( Events.StartingCharacterCreation, SetLayerToGrid );
        EventManager.StopListening( Events.FinishedCharacterCreating, SetLayerToIgnore );
    }

    private void SetLayerToIgnore ()
    {
        gameObject.layer = LayerMask.NameToLayer( "Ignore Raycast" );
    }

    private void SetLayerToGrid ()
    {
        gameObject.layer = LayerMask.NameToLayer( "Grid" );
    }
}

using UnityEngine;
using System.Collections;

public class LayerController : MonoBehaviour
{
    public void Start ()
    {
        SnapCharacter.MovingCharacter += SetLayerToGrid;
        SnapCharacter.MovedCharacter += SetLayerToIgnore;
    }

    public void OnEnable ()
    {
        EventManager.StartListening( "StartingCharacterCreation", SetLayerToGrid );
        EventManager.StartListening( "FinishedCharacterCreating", SetLayerToIgnore );
    }

    public void OnDisable ()
    {
        EventManager.StopListening( "StartingCharacterCreation", SetLayerToGrid );
        EventManager.StopListening( "FinishedCharacterCreating", SetLayerToIgnore );
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

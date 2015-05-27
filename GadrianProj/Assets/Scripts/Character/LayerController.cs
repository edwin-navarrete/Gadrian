﻿using UnityEngine;
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
        EventManager.StartListening( Events.StartingCharacterCreation, SetLayerToGrid );
        EventManager.StartListening( Events.FinishedCharacterCreating, SetLayerToIgnore );
    }

    public void OnDisable ()
    {
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

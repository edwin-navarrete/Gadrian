using UnityEngine;
using System.Collections;

public class UndoButton : MonoBehaviour 
{
	public void OnEnable ()
	{
        EventManager.StartListening( Events.FinishingCharacterPlacement, TurnOn );
	}

    public void OnDisable ()
    {
        EventManager.StopListening( Events.FinishingCharacterPlacement, TurnOn );
    }

	private void TurnOn ()
	{
		gameObject.SetActive (true);
	}
}

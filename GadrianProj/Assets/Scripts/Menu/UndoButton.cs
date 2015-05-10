using UnityEngine;
using System.Collections;

public class UndoButton : MonoBehaviour 
{
	void Start ()
	{
		CharacterManager.Instance.FinishingCharacterPlacement += TurnOn;
	}

	void TurnOn ()
	{
		gameObject.SetActive (true);
	}
}

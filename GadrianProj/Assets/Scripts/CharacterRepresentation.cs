using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterRepresentation : MonoBehaviour
{
	private Image body;
	private Image complexion;
	private CharacterManager manager;

	private void Awake ()
	{
		TurnOff ();

		manager = CharacterManager.Instance;
	}

	#region OnEnable/OnDisable

	private void OnEnable ()
	{
		manager.StartingDrag += TurnOn;
		manager.FinishingDrag += TurnOff;
	}

	private void OnDisable ()
	{
		manager.StartingDrag -= TurnOn;
		manager.FinishingDrag -= TurnOff;
	}

	#endregion

	private void TurnOn (Sprite body, Sprite complexion)
	{
		this.body.color = Color.white;
		this.complexion.color = Color.white;
		this.body.sprite = body;
		this.complexion.sprite = complexion;
	}

	private void TurnOff ()
	{
		this.body.color = Color.clear;
		this.complexion.color = Color.clear;
		this.body.sprite = null;
		this.complexion.sprite = null;
	}
}

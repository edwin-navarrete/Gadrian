using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterRepresentation : MonoBehaviour
{
	[SerializeField]
	private Image body;
	[SerializeField]
	private Image complexion;

	private void Awake ()
	{
		TurnOff ();
	}

	#region OnEnable/OnDisable

	private void OnEnable ()
	{
		CharacterManager.Instance.StartingDrag += TurnOn;
		CharacterManager.Instance.FinishingDrag += TurnOff;
	}

	private void OnDisable ()
	{
		CharacterManager.Instance.StartingDrag -= TurnOn;
		CharacterManager.Instance.FinishingDrag -= TurnOff;
	}

	#endregion

	private void TurnOn (Sprite body, Sprite complexion)
	{
		this.body.enabled = true;
		this.complexion.enabled = true;
		this.body.sprite = body;
		this.complexion.sprite = complexion;
	}

	private void TurnOff ()
	{
		this.transform.position = new Vector2 ( 0, Screen.height + 100 );
		this.body.sprite = null;
		this.complexion.sprite = null;
		this.body.enabled = false;
		this.complexion.enabled = false;
	}
}

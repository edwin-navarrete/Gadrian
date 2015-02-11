using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterRepresentation : MonoBehaviour
{
	private Image image;
	[SerializeField]
	private CharacterManager manager;

	private void Awake ()
	{
		image = GetComponent<Image> ();

		if ( image != null )
		{
			image.color = Color.clear;
		}

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

	private void TurnOn (Sprite characterImage)
	{
		image.color = Color.white;
		image.sprite = characterImage;
	}

	private void TurnOff ()
	{
		image.color = Color.clear;
		image.sprite = null;
	}
}

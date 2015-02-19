using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	public new Text name;
	public Image icon;
	public Button button;
	public Personality personality;

	[SerializeField]
	private CharacterManager manager;

	private void Awake ()
	{
		manager = CharacterManager.Instance;
	}


	#region Miembros de IPointerDownHandler

	public void OnPointerDown (PointerEventData eventData)
	{
		manager.SetCharacterImage ( icon.sprite, eventData );
	}

	#endregion

	#region Miembros de IDragHandler

	public void OnDrag (PointerEventData eventData)
	{
		manager.MoveCharacterImage ( eventData );
	}

	#endregion

	#region Miembros de IPointerUpHandler

	public void OnPointerUp (PointerEventData eventData)
	{
		manager.PlaceCharacterImage ( eventData, gameObject, personality, name.text );
	}

	#endregion
}

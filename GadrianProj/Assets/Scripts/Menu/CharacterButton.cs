using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	public Image body;
	public Image complexion;
	public Image face;
	public Personality personality;     //Personallity is in child object

	private CharacterManager manager;

	private void Awake ()
	{
		manager = CharacterManager.Instance;
	}

	#region Miembros de IPointerDownHandler

	public void OnPointerDown (PointerEventData eventData)
	{
		manager.SetCharacterImage ( body.sprite, complexion.sprite, eventData );
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
		manager.PlaceCharacterImage ( eventData, gameObject, personality, "" );
	}

	#endregion
}

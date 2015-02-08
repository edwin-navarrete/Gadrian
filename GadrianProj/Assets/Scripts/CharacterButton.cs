using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterButton : MonoBehaviour, IPointerDownHandler
{
	public new Text name;
	public Image icon;
	public Button button;

	public UnityEngine.Events.UnityEvent<Sprite, PointerEventData> PointerDown;
	[SerializeField]
	private CharacterManager manager;

	private void Awake ()
	{
		//manager = GameObject.Find ( "Character manager" ).GetComponent<CharacterManager> ();
		if ( manager != null )
		{
			//PointerDown.AddListener ( manager.SetCharacterImage );
		}
	}

	public void OnDisable ()
	{
		//PointerDown.AddListener ( manager.SetCharacterImage );
	}

	public void OnEnable ()
	{
		//PointerDown.RemoveListener ( manager.SetCharacterImage );
	}

	#region Miembros de IPointerDownHandler

	public void OnPointerDown (PointerEventData eventData)
	{
		PointerDown.Invoke ( icon.sprite, eventData );
	}

	#endregion
}

using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;
using System.Collections;

public class CharacterManager : MonoBehaviour//, IDragHandler, IPointerUpHandler
{
	private Vector2 pointerOffset;

	[SerializeField]
	private RectTransform canvasRectTransform;
	[SerializeField]
	private RectTransform characterRectTransform;

	//public UnityEvent<Sprite> StartingDrag;
	//public UnityEvent<PointerEventData> FinishingDrag;

	public event UnityEngine.Events.UnityAction<Sprite> StartingDrag;
	public event UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData> FinishingDrag;

	private GameObject lastCharSelected;	

	private static CharacterManager instance;

	public static CharacterManager Instance
	{
		get
		{
			if ( instance == null )
			{
				instance = GameObject.FindObjectOfType<CharacterManager> ();

				if ( instance == null )
				{
					GameObject newGO = new GameObject ( "Character manager" );
					instance = newGO.AddComponent<CharacterManager> ();
				}

				DontDestroyOnLoad ( instance );
			}

			return instance;
		}
	}

	public void Awake ()
	{
		if ( CharacterManager.Instance != this )
			Destroy ( this.gameObject );

		canvasRectTransform = GameObject.FindObjectOfType<Canvas> ().transform as RectTransform;
		characterRectTransform = GameObject.FindObjectOfType<CharacterRepresentation> ().transform as RectTransform;
	}

	/*
	#region Miembros de IDragHandler

	public void OnDrag (PointerEventData eventData)
	{
		if ( characterRectTransform == null )
			return;

		Vector2 pointerPosition = ClampToWindow ( eventData );

		Vector2 localPointerPosition;
		if ( RectTransformUtility.ScreenPointToLocalPointInRectangle (
			canvasRectTransform, pointerPosition, eventData.pressEventCamera, out localPointerPosition ) )
		{
			characterRectTransform.localPosition = localPointerPosition - pointerOffset;
		}
	}

	private Vector2 ClampToWindow (PointerEventData eventData)
	{
		Vector2 rawPointerPosition = eventData.position;

		Vector3[] canvasCorners = new Vector3[4];
		canvasRectTransform.GetWorldCorners ( canvasCorners );

		float clampedX = Mathf.Clamp ( rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x );
		float clampedY = Mathf.Clamp ( rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y );

		Vector2 newPointerPosition = new Vector2 ( clampedX, clampedY );
		return newPointerPosition;
	}

	#endregion

	#region Miembros de IPointerUpHandler

	public void OnPointerUp (PointerEventData eventData)
	{
		FinishingDrag.Invoke ( eventData );
	}

	#endregion
	 * */

	/// <summary>
	/// When input from touch/mouse is down, this method will turn off the element of the scroll list and then create a object
	/// to drag around and finally place on the grid.
	/// </summary>
	/// <param name="characterImage"></param>
	/*public void SetCharacterImage (Sprite characterImage, PointerEventData eventData)
	{
		characterRectTransform.GetComponent<Image> ().sprite = characterImage;

		if ( eventData.selectedObject.transform.tag == "ScrollListElement" )
		{
			lastCharSelected = eventData.selectedObject;
			lastCharSelected.SetActive ( false );

			RectTransform listElementRectTransform = eventData.selectedObject.transform as RectTransform;

			RectTransformUtility.ScreenPointToLocalPointInRectangle (
				listElementRectTransform, eventData.position, eventData.pressEventCamera, out pointerOffset );
		}
	}*/
}

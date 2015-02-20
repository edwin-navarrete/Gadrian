using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
	[SerializeField]
	private RectTransform canvasRectTransform;
	[SerializeField]
	private RectTransform characterRectTransform;
	[SerializeField]
	private GFGrid grid;

	private GameObject lastCharSelected;		// Object from the scroll list that was last selected
	private Transform CharacterPlaceholder;		// Parent in hierarchy to make all characters children of

	private List<Personality> characters;		// Reference to all characters placed in world

	#region Events

	public event UnityEngine.Events.UnityAction<Sprite> StartingDrag;
	public event UnityEngine.Events.UnityAction FinishingDrag;

	private void OnStartingDrag (Sprite image)
	{
		if ( StartingDrag != null )
		{
			StartingDrag (image);
		}
	}

	private void OnFinishingDrag ()
	{
		if ( FinishingDrag != null )
		{
			FinishingDrag ();
		}
	}

	#endregion
	
	#region Singleton

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

	#endregion

	// This awake method could be remove if the character manager already have reference in Editor to canvasRectTransform,
	// characterRectTransform and CharacterPlaceholder
	public void Awake ()
	{
		if ( CharacterManager.Instance != this )
			Destroy ( this.gameObject );

		canvasRectTransform = GameObject.FindObjectOfType<Canvas> ().transform as RectTransform;
		characterRectTransform = GameObject.FindObjectOfType<CharacterRepresentation> ().transform as RectTransform;
		CharacterPlaceholder = GameObject.FindGameObjectWithTag ( "Placeholder" ).transform;
	}

	public Vector3 AskGridPosition (Vector3 position)
	{
		return grid.WorldToGrid ( position );
	}

	#region Character movement from scroll list to world

	/// <summary>
	/// When input from touch/mouse is down, this method will turn off the element of the scroll list 
	/// and then create a object to drag around and finally place on the grid.
	/// </summary>
	/// <param name="characterImage"></param>
	public void SetCharacterImage (Sprite characterImage, PointerEventData eventData)
	{
		OnStartingDrag ( characterImage );

		characterRectTransform.GetComponent<Image> ().sprite = characterImage;
		characterRectTransform.position = eventData.position;		
	}

	public void MoveCharacterImage (PointerEventData eventData)
	{
		if ( characterRectTransform == null )
			return;

		Vector2 pointerPosition = ClampToWindow ( Input.mousePosition );

		Vector2 localPointerPosition;
		if ( RectTransformUtility.ScreenPointToLocalPointInRectangle (
			canvasRectTransform, pointerPosition, eventData.pressEventCamera, out localPointerPosition ) )
		{
			characterRectTransform.localPosition = localPointerPosition;
		}
	}

	public void PlaceCharacterImage (PointerEventData eventData, GameObject sender, Personality personality, string charName)
	{
		OnFinishingDrag ();

		RaycastHit hit;

		if ( Physics.Raycast ( Camera.main.ScreenPointToRay ( Input.mousePosition), out hit, Mathf.Infinity ) )
		{
			if ( hit.transform.tag == "Grid" )
			{
				GameObject newCharacter = new GameObject ( charName, typeof ( SpriteRenderer ) );
				newCharacter.transform.position = hit.point;
				newCharacter.transform.localScale = new Vector3 ( 0.7773f, 0.7219f, 1f );

				CircleCollider2D circleCollider = newCharacter.AddComponent<CircleCollider2D> ();
				circleCollider.radius = 1f;
				circleCollider.isTrigger = true;

				newCharacter.AddComponent<SnapCharacter> ();
				newCharacter.AddComponent<MoodHandler> ();
				Personality newCharPersonality = newCharacter.AddComponent<Personality> ();
				newCharPersonality.CopyPersonality ( personality );
				newCharPersonality.TraitsEffect ();
				characters.Add ( newCharPersonality );

				newCharacter.transform.SetParent ( CharacterPlaceholder );
				sender.SetActive ( false );
			}
		}
	}

	private Vector2 ClampToWindow (Vector3 mousePoistion)
	{
		Vector2 rawPointerPosition = mousePoistion;

		Vector3[] canvasCorners = new Vector3[4];
		canvasRectTransform.GetWorldCorners ( canvasCorners );

		float clampedX = Mathf.Clamp ( rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x );
		float clampedY = Mathf.Clamp ( rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y );

		Vector2 newPointerPosition = new Vector2 ( clampedX, clampedY );
		return newPointerPosition;
	}

	#endregion
}

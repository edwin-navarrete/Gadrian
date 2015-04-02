using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CharacterItem : MonoBehaviour
{
	[Range ( 1, 12)]
	public int characterAmount = 5;

	[SerializeField]
	private List<string> charactersName;

	[SerializeField]
	private GameObject uiCharacterPrefab;

	[SerializeField]
	private Transform contentPanel;

	private ShutDown shutDown;

	private List<int> selec;
	private int index;

    public void Awake ()
    {
        shutDown = GetComponent<ShutDown>();
    }

	private void Start ()
	{
		selec = new List<int>();
		selec.Add(1);
		selec.Add(2);
		selec.Add(5);
		selec.Add(6);
		selec.Add(8);

		PopulateScrollList ();
	}

	/// <summary>
	/// Populate list with button prefab instances and make them child of the content panel.
	/// Also connect references using the CharacterButton help to the Character class.
	/// </summary>
	private void PopulateScrollList ()
	{		 
		for ( int i = 0; i < characterAmount; i++ )
		{
			CreateCharacterItem( null );
			index++;
		}
		// Start checking if there is no more childs active to shut down gameObject
		shutDown.StartCheck ();
	}

	public void CreateCharacterItem (Personality personality)
	{
		GameObject newChar = Instantiate ( uiCharacterPrefab ) as GameObject;
		
		CharacterButton charButton = newChar.GetComponent<CharacterButton> ();
		//Debug.LogWarning("Choosing "+persIdx);
		if (personality == null)
		{
			charButton.personality.SetupPersonality ( PersonalityManager.PersonalityModel, selec[index] );
		}
		else
		{
			charButton.personality.CopyPersonality ( personality );
		}

		// Modify UICharacter in itemList
		charButton.personality.TraitsEffect ();
		
		newChar.transform.SetParent ( contentPanel );
	}
}
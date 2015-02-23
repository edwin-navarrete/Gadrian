using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CharacterItem : MonoBehaviour
{
	[Range ( 1, 12)]
	public int characterAmount = 4;

	[SerializeField]
	private List<string> charactersName;

	[SerializeField]
	private GameObject uiCharacterPrefab;

	[SerializeField]
	private Transform contentPanel;

	private ShutDown shutDown;

	private void Start ()
	{
		shutDown = GetComponent<ShutDown> ();
		PopulateScrollList ();
	}

	/// <summary>
	/// Populate list with button prefab instances and make them child of the content panel.
	/// Also connect references using the CharacterButton help to the Character class.
	/// </summary>
	private void PopulateScrollList ()
	{
		//UnityEngine.Random.Range ( 0, model.PersonalityCnt  ) 
		for ( int i = 0; i < characterAmount; i++ )
		{
			GameObject newChar = Instantiate ( uiCharacterPrefab ) as GameObject;

			CharacterButton charButton = newChar.GetComponent<CharacterButton> ();
			// charButton.name.text = charactersName[UnityEngine.Random.Range ( 0, charactersName.Count )];

			charButton.personality.SetupPersonality ( 
			     PersonalityManager.PersonalityModel, 
			     UnityEngine.Random.Range ( 0, PersonalityManager.PersonalityModel.PersonalityCnt  )  );
			charButton.personality.TraitsEffect ();

			newChar.transform.SetParent ( contentPanel );
		}
		shutDown.StartCheck ();
	}
}
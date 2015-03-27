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

    public void Awake ()
    {
        shutDown = GetComponent<ShutDown>();
    }

	private void Start ()
	{
		PopulateScrollList ();
	}

	/// <summary>
	/// Populate list with button prefab instances and make them child of the content panel.
	/// Also connect references using the CharacterButton help to the Character class.
	/// </summary>
	private void PopulateScrollList ()
	{
		List<int> selec = new List<int>();
		selec.Add(1);
		selec.Add(2);
		selec.Add(5);
		selec.Add(6);
		selec.Add(8);
		 
		//UnityEngine.Random.Range ( 0, model.PersonalityCnt  ) 
		for ( int i = 0; i < characterAmount; i++ )
		{
			GameObject newChar = Instantiate ( uiCharacterPrefab ) as GameObject;

			CharacterButton charButton = newChar.GetComponent<CharacterButton> ();
			// charButton.name.text = charactersName[UnityEngine.Random.Range ( 0, charactersName.Count )];
			// int persIdx = UnityEngine.Random.Range ( 0, PersonalityManager.PersonalityModel.PersonalityCnt  );
			int persIdx = selec[i];
			//Debug.LogWarning("Choosing "+persIdx);
			charButton.personality.SetupPersonality ( 
			     PersonalityManager.PersonalityModel, 
			                                         persIdx
			     );
			// Modify UICharacter in itemList
			charButton.personality.TraitsEffect ();

			newChar.transform.SetParent ( contentPanel );
		}
		// Start checking if there is no more childs active to shut down gameObject
		shutDown.StartCheck ();
	}
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character
{
	public string name;
	public Sprite icon;
	public Button.ButtonClickedEvent onClick;
}

public class CharacterItem : MonoBehaviour
{
	[SerializeField]
	private GameObject characterPrefab;
	[SerializeField]
	private List<Character> characterList;

	[SerializeField]
	private Transform contentPanel;

	void Start ()
	{
		PopulateScrollList ();
	}

	/// <summary>
	/// Populate list with button prefab instances and make them child of the content panel.
	/// Also connect references using the CharacterButton help to the Character class.
	/// </summary>
	private void PopulateScrollList ()
	{
		foreach ( Character character in characterList )
		{
			GameObject newChar = Instantiate ( characterPrefab ) as GameObject;
			CharacterButton charButton = newChar.GetComponent<CharacterButton> ();

			charButton.name.text = character.name;
			charButton.icon.sprite = character.icon;
			charButton.button.onClick = character.onClick;

			newChar.transform.SetParent ( contentPanel );
		}
	}
}
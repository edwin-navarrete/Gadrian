using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharacterComplexion
{
	public string name;
	public Sprite icon;
}

public class CharacterItem : MonoBehaviour
{
	[SerializeField]
	private GameObject characterPrefab;
	[SerializeField]
	private List<CharacterComplexion> characterList;

	[SerializeField]
	private Transform contentPanel;

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
		foreach ( CharacterComplexion character in characterList )
		{
			GameObject newChar = Instantiate ( characterPrefab ) as GameObject;
			CharacterButton charButton = newChar.GetComponent<CharacterButton> ();

			charButton.name.text = character.name;
			charButton.icon.sprite = character.icon;

			newChar.transform.SetParent ( contentPanel );
		}
	}
}
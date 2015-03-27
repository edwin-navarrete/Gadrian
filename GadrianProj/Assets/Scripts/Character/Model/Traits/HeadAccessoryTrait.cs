using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class HeadAccessoryTrait : Trait, IEquatable<HeadAccessoryTrait>
{
	private Sprite headAccessory;

	#region Constructors

	public HeadAccessoryTrait (Sprite headAccessory)
	{
		this.headAccessory = headAccessory;
	}

	//public HeadAccessoryTrait (HeadAccessoryTrait traitToCopy)
	//	: this ( traitToCopy.headAccessory )	{ }

	public HeadAccessoryTrait ()
		: this ( null )	{ }

	#endregion

	#region Miembros de IEquatable<HeadAccessoryTrait>

	public bool Equals (HeadAccessoryTrait other)
	{
		if ( other == null )
			return false;
		return headAccessory.Equals ( other.headAccessory );
	}

	#endregion

	#region Miembros de Trait

	public void AffectCharacter (GameObject character)
	{
		Transform complexionTrans = character.transform.FindChild ( "Head Accessory" );
		SpriteRenderer characterComplexion = complexionTrans.GetComponent<SpriteRenderer> ();
		if ( characterComplexion == null )
		{
			Image characterSpriteComplexion = complexionTrans.GetComponent<Image> ();
			characterSpriteComplexion.sprite = headAccessory;
		}
		else
			characterComplexion.sprite = headAccessory;
	}

	#endregion
}
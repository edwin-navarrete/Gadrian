using UnityEngine;
using System;
using System.Collections;

/**
 * Represents a particular etnia by setting the skin color 
 **/
// NOTE EtniaTrait shoudn't be a MonoBehabiour becuase EtniaFactor uses constructor of it 
public class EtniaTrait : Trait, IEquatable<EtniaTrait>
{
	Color skinColor;

	public EtniaTrait(Color skinColor){
		this.skinColor = skinColor;
	}

	public EtniaTrait (EtniaTrait traitToCopy)
		: this ( traitToCopy.skinColor ) { }

	public bool Equals(EtniaTrait other){
		if(other == null)
			return false;
		return skinColor.Equals(other.skinColor);
	}

	public void AffectCharacter (GameObject character)
	{
		Transform etniaTrans = character.transform.FindChild ( "Body" );
		SpriteRenderer spriteRenderer = etniaTrans.GetComponent<SpriteRenderer> ();
		spriteRenderer.material.color = skinColor;
	}
}


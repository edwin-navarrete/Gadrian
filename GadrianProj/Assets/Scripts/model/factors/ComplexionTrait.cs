using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
	Sprite complexion;

	///FIXME resolve how to represent complexion on characters, either sprite or scale

	public ComplexionTrait (Sprite complexion)
	{
		this.complexion = complexion;
	}

	public ComplexionTrait (ComplexionTrait traitToCopy)
		: this ( traitToCopy.complexion ) { }

	#region Miembros de IEquatable<ContextTrait>

	public bool Equals (ComplexionTrait other)
	{
		if ( other == null )
			return false;
		return complexion.Equals ( other.complexion );
	}

	#endregion

	#region Miembros de Trait

	public void AffectCharacter (GameObject character)
	{
		SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = complexion;
	}

	#endregion
}

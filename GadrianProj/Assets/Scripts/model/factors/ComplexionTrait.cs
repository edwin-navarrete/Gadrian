using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
	private Sprite complexion;

	#region Constructors

	public ComplexionTrait (Sprite complexion)
	{
		this.complexion = complexion;
	}

	public ComplexionTrait (ComplexionTrait traitToCopy)
		: this ( traitToCopy.complexion ) { }

	#endregion

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
		Transform complexionTrans = character.transform.FindChild ( "Complexion" );
		Image characterComplexion =	complexionTrans.GetComponent<Image> ();
		characterComplexion.sprite = complexion;
	}

	#endregion
}

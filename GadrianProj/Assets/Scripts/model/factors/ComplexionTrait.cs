using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
	private Sprite complexion;
	private Sprite body;

	#region Constructors

	public ComplexionTrait (Sprite body, Sprite complexion)
	{
		this.body = body;
		this.complexion = complexion;
	}

	public ComplexionTrait (ComplexionTrait traitToCopy)
		: this ( traitToCopy.body, traitToCopy.complexion ) { }

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
		SpriteRenderer characterComplexion = complexionTrans.GetComponent<SpriteRenderer> ();
		if ( characterComplexion == null )
		{
			Image characterSpriteComplexion = complexionTrans.GetComponent<Image> ();
			characterSpriteComplexion.sprite = complexion;
		}
		else
			characterComplexion.sprite = complexion;

		Transform bodyTrans = character.transform.FindChild ( "Body" );
		SpriteRenderer characterBody = bodyTrans.GetComponent<SpriteRenderer> ();
		if ( characterBody == null )
		{
			Image characterSpriteBody = complexionTrans.GetComponent<Image> ();
			characterSpriteBody.sprite = body;
		}
		else
			characterBody.sprite = body;
	}

	#endregion
}

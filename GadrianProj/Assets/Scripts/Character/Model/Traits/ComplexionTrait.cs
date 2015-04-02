using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
	private Sprite complexion;
	private Sprite body;
	private Sprite uiComplexion;
	private Sprite uiBody;
	private RectTransform facePosition;

	#region Constructors

	public ComplexionTrait (Sprite body, Sprite complexion, Sprite uiBody, Sprite uiComplexion, RectTransform facePosition)
	{
		this.body = body;
		this.complexion = complexion;
		this.uiBody = uiBody;
		this.uiComplexion = uiComplexion;
		this.facePosition = facePosition;
	}

	public ComplexionTrait (ComplexionTrait traitToCopy)
		: this ( traitToCopy.body, traitToCopy.complexion, traitToCopy.uiBody, traitToCopy.uiComplexion, traitToCopy.facePosition ) { }

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
			characterSpriteComplexion.sprite = uiComplexion;
		}
		else
			characterComplexion.sprite = complexion;

		Transform bodyTrans = character.transform.FindChild ( "Body" );
		SpriteRenderer characterBody = bodyTrans.GetComponent<SpriteRenderer> ();
		if ( characterBody == null )
		{
			Image characterSpriteBody = bodyTrans.GetComponent<Image> ();
			characterSpriteBody.sprite = uiBody;
		}
		else
			characterBody.sprite = body;

		Transform faceTrans = character.transform.FindChild ( "Face" );
		Image characterFace = faceTrans.GetComponent<Image> ();
		if ( characterFace != null )
		{
			RectTransform uiFaceTrans = faceTrans as RectTransform;
			uiFaceTrans.localPosition = facePosition.localPosition;
		}
	}

	#endregion
}

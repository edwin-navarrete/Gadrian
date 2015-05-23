using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
    private Sprite bodyHair;
    private BodyMatch bodyMatch;

    #region Constructors

    public ComplexionTrait (Sprite bodyHair)
    {
        this.bodyHair = bodyHair;
    }

    public ComplexionTrait (ComplexionTrait traitToCopy)
        : this( traitToCopy.bodyHair ) { }

    #endregion

    #region Miembros de IEquatable<ContextTrait>

    public bool Equals (ComplexionTrait other)
    {
        if ( other == null )
            return false;

        return bodyMatch.ComplexionValue.Equals( other.bodyMatch.ComplexionValue );
    }

    #endregion

    #region Miembros de Trait

    public void AffectCharacter (GameObject character)
    {
        bodyMatch = character.GetComponent<BodyMatch>();
        bodyMatch.Complexion = this;

        //SpriteRenderer bodyHairRenderer = character.transform.FindChild("Body Hair").GetComponent<SpriteRenderer>();
        //if (bodyHairRenderer != null)
        //{
        //    bodyHairRenderer.sprite = this.bodyHair;
        //}

        Face characterFace = character.transform.FindChild( "Face" ).GetComponent<Face>();
        if ( characterFace != null )
        {
            characterFace.LocateFace( this );
        }
    }

    #endregion
}

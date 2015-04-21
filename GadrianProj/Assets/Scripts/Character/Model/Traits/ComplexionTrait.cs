using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
    private RectTransform facePosition;
    private BodyMatch bodyMatch;

    #region Constructors

    public ComplexionTrait (RectTransform facePosition)
    {
        this.facePosition = facePosition;
    }

    public ComplexionTrait (ComplexionTrait traitToCopy)
        : this( traitToCopy.facePosition ) { }

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

        Transform faceTrans = character.transform.FindChild( "Face" );
        Face characterFace = faceTrans.GetComponent<Face>();
        if ( characterFace != null )
        {
            characterFace.LocateFace( this );
        }
    }

    #endregion
}

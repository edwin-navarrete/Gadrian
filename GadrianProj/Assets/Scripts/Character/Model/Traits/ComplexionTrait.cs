using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class ComplexionTrait : Trait, IEquatable<ComplexionTrait>
{
    private int complexionValue;
    private BodyMatch bodyMatch;

    #region Constructors

    public ComplexionTrait (int complexionValue)
    {
        this.complexionValue = complexionValue;
    }

    public ComplexionTrait (ComplexionTrait traitToCopy)
        : this( traitToCopy.complexionValue ) { }

    public override string ToString()
    {
        if (this == ComplexionFactor.SMALL)
            return "Small";
        if (this == ComplexionFactor.FAT)
            return "Fat";
        if (this == ComplexionFactor.TALL)
            return "Tall";
        return "UnkComplexion";
    }

    #endregion

    #region Miembros de IEquatable<ContextTrait>

    public bool Equals (ComplexionTrait other)
    {
        if ( other == null )
            return false;

        return complexionValue.Equals( other.complexionValue );
    }

    #endregion

    #region Miembros de Trait

    public void AffectCharacter (GameObject character)
    {
        bodyMatch = character.GetComponent<BodyMatch>();
        bodyMatch.Complexion = complexionValue;

        Face characterFace = character.transform.FindChild( "Face" ).GetComponent<Face>();
        if ( characterFace != null )
        {
            // The Face know where to locate the transform depending on the complexionValue
            characterFace.LocateFace( complexionValue );
        }
    }

    #endregion
}

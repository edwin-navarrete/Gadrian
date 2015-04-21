using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/**
 * Represents a particular etnia by setting the skin color 
 **/
// NOTE EtniaTrait shoudn't be a MonoBehabiour becuase EtniaFactor uses constructor of it 
public class EtniaTrait : Trait, IEquatable<EtniaTrait>
{
    Color skinColor;
    BodyMatch bodyMatch;

    #region Constructors

    public EtniaTrait (Color skinColor)
    {
        this.skinColor = skinColor;
    }

    public EtniaTrait (EtniaTrait traitToCopy)
        : this( traitToCopy.skinColor ) { }

    #endregion

    public override string ToString ()
    {
        return "Etnia: " + skinColor;
    }

    #region Miembros de IEquetable<EtniaTrait>

    public bool Equals (EtniaTrait other)
    {
        if ( other == null )
            return false;

        return bodyMatch.EtniaValue.Equals( other.bodyMatch.EtniaValue );
    }

    #endregion

    public void AffectCharacter (GameObject character)
    {
        bodyMatch = character.GetComponent<BodyMatch>();
        bodyMatch.Etnia = this;
    }
}


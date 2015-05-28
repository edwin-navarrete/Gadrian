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
    int etniaValue;
    BodyMatch bodyMatch;

    #region Constructors

    public EtniaTrait (int etniaValue)
    {
        this.etniaValue = etniaValue;
    }

    public EtniaTrait (EtniaTrait traitToCopy)
        : this( traitToCopy.etniaValue ) { }

    #endregion

    public override string ToString ()
    {
        if (this == EtniaFactor.BROWN)
            return "Brown";
        if (this == EtniaFactor.GOLD)
            return "Gold";
        if (this == EtniaFactor.RED)
            return "Red";
        return "UnkEtnia";
    }

    #region Miembros de IEquetable<EtniaTrait>

    public bool Equals (EtniaTrait other)
    {
        if ( other == null )
            return false;

        return etniaValue.Equals( other.etniaValue );
    }

    #endregion

    public void AffectCharacter (GameObject character)
    {
        bodyMatch = character.GetComponent<BodyMatch>();
        bodyMatch.Etnia = etniaValue;
    }
}


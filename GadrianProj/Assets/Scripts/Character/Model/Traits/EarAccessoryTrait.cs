using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class EarAccessoryTrait : Trait, IEquatable<EarAccessoryTrait>
{
	private Sprite earAccessory;

	#region Constructors

	public EarAccessoryTrait (Sprite earAccessory)
	{
		this.earAccessory = earAccessory;
	}

    public override string ToString()
    {
        if (this == EarAccessoryFactor.FURRY)
            return "Furry";
        if (this == EarAccessoryFactor.NORMAL)
            return "Normal";
        if (this == EarAccessoryFactor.POINTY)
            return "Pointy";
        return "UnkEars";
    }


//	public EarAccessoryTrait (EarAccessoryTrait traitToCopy)
//		: this ( traitToCopy.earAccessory )	{ }

	public EarAccessoryTrait ()
		: this ( null )	{ }

	#endregion

	#region Miembros de IEquatable<HeadAccessoryTrait>

	public bool Equals (EarAccessoryTrait other)
	{
		if ( other == null )
			return false;
		return earAccessory.Equals ( other.earAccessory );
	}

	#endregion

	#region Miembros de Trait

	public void AffectCharacter (GameObject character)
	{
		Transform faceTranform = character.transform.FindChild ( "Face" );
		Transform earsTranform = faceTranform.FindChild ( "Ears" );
		if (earsTranform != null) 
		{
			SpriteRenderer earsRenderer = earsTranform.GetComponent<SpriteRenderer> ();
			earsRenderer.sprite = earAccessory;
		}
		else
		{
			Debug.LogError("No se encontraron las orejas");
		}
	}

	#endregion
}
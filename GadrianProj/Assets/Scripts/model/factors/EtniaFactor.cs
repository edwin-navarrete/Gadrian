using UnityEngine;
using System;
using Random = UnityEngine.Random;

/**
 * Represents the color of skin as a trait
 * */
public class EtniaFactor : IPersonalityFactor
{
	public readonly static EtniaTrait YELLOW = new EtniaTrait(Color.yellow);
	public readonly static EtniaTrait LEMON = new EtniaTrait(new Color(0,0,0)); //FIXME YellowGreen
	public readonly static EtniaTrait GOLD = new EtniaTrait(new Color(0,0,0)); //FIXME .Gold;

	private static EtniaTrait lastTrait;

	public Mood confront(ITrait a, ITrait b){
		//FIXME
		var etnA = a as EtniaTrait;
		var etnB = a as EtniaTrait;
		return Mood.INDIFERENT;
	}

	// Allow to get a initialiced instances of Trait without repeating the last one
	public ITrait GetRandomTrait ()
	{
		int traitToReturn = Random.Range ( 0, 3 );
		switch ( traitToReturn )
		{
			case 0:
				lastTrait = CheckValid ( YELLOW );
				break;

			case 1:
				lastTrait = CheckValid ( LEMON );
				break;

			case 2:
				lastTrait = CheckValid ( GOLD );
				break;

			default:
				lastTrait = CheckValid ( YELLOW );
				Debug.Log ( "traitToReturn has taken a non-valid value" );
				break;
		}
		return lastTrait as ITrait;
	}

	// Checks if the instance which is about to be return is the same as the last one
	private EtniaTrait CheckValid (EtniaTrait trait)
	{
		if ( trait != lastTrait )
			return new EtniaTrait ( trait );
		else
			return GetRandomTrait () as EtniaTrait;
	}
}


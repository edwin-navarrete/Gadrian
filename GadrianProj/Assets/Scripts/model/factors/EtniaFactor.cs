using UnityEngine;
using System;
using System.Collections.Generic;

/**
 * Represents the color of skin as a trait
 * */
public class EtniaFactor : PersonalityFactor
{
	public static EtniaTrait YELLOW;
	public static EtniaTrait LEMON;
	public static EtniaTrait GOLD;
	[SerializeField]
	private Color yellow;
	[SerializeField]
	private Color lemon;
	[SerializeField]
	private Color gold;

	private readonly static List<Trait> etniaTraits = new List<Trait>();

	private void Awake ()
	{
		YELLOW = new EtniaTrait ( yellow );
		LEMON = new EtniaTrait ( lemon );
		GOLD = new EtniaTrait ( gold );

		etniaTraits.Add(YELLOW);
		etniaTraits.Add(LEMON);
		etniaTraits.Add(GOLD);
	}

	public override List<Trait> getTraits(){
		return etniaTraits;
	}

	protected override Mood face(Trait a, Trait b){
		var etnA = a as EtniaTrait;
		var etnB = a as EtniaTrait;
		if(etnA == null || etnB == null){
			throw new UnityException("Facing Invalid Etnia Traits");
		}

		// LEMON fears YELLOW
		// GOLD fears LEMON
		if(LEMON.Equals(etnA) && YELLOW.Equals(etnB)
		   || GOLD.Equals(etnA) && LEMON.Equals(etnB)){
			return Mood.SCARED;
		}

		// LEMON hates GOLD
		// YELLOW hates LEMON
		// YELLOW hates GOLD
		// GOLD hates YELLOW
		return Mood.ANGRY;
	}

}


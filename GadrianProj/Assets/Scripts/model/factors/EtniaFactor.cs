using UnityEngine;
using System;
using System.Collections.Generic;

/**
 * Represents the color of skin as a trait
 * */
public class EtniaFactor : PersonalityFactor
{
	public readonly static EtniaTrait YELLOW = new EtniaTrait(Color.yellow);
	public readonly static EtniaTrait LEMON = new EtniaTrait(new Color(0,0,0)); //FIXME YellowGreen
	public readonly static EtniaTrait GOLD = new EtniaTrait(new Color(0,0,0)); //FIXME .Gold;
	private readonly static List<Trait> etniaTraits = new List<Trait>();

	static EtniaFactor(){
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


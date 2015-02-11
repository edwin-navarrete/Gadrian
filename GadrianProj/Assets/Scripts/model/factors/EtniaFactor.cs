using UnityEngine;
using System;

/**
 * Represents the color of skin as a trait
 * */
public class EtniaFactor : IPersonalityFactor
{
	public static EtniaTrait YELLOW = new EtniaTrait(Color.yellow);
	public static EtniaTrait LEMON = new EtniaTrait(new Color(0,0,0)); //FIXME YellowGreen
	public static EtniaTrait GOLD = new EtniaTrait(new Color(0,0,0)); //FIXME .Gold;
	

	public Mood confront(ITrait a, ITrait b){
		//FIXME
		var etnA = a as EtniaTrait;
		var etnB = a as EtniaTrait;
		return Mood.INDIFERENT;
	}
}


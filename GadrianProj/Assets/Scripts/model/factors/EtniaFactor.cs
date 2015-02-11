using System;

/**
 * Represents the color of skin as a trait
 * */
public class EtniaFactor : IPersonalityFactor
{
	public const EtniaTrait YELLOW = new EtniaTrait(Color.Yellow);
	public const EtniaTrait LEMON = new EtniaTrait(Color.YellowGreen);
	public const EtniaTrait GOLD = new EtniaTrait(Color.Gold);

	Mood confront(Trait a, Trait b){
		var etnA = a as EtniaTrait;
		var etnB = a as EtniaTrait;
	}
}


using UnityEngine;
using System.Collections.Generic;

public class ComplexionFactor : PersonalityFactor
{
	public readonly static ComplexionTrait SMALL;
	public readonly static ComplexionTrait FAT;
	public readonly static ComplexionTrait TALL;
	public readonly static ComplexionTrait AVERAGE;

	private readonly static List<Trait> etniaTraits = new List<Trait>();

	static ComplexionFactor()
	{
		etniaTraits.Add(SMALL);
		etniaTraits.Add(FAT);
		etniaTraits.Add(TALL);
		etniaTraits.Add(AVERAGE);
	}

	public override List<Trait> getTraits(){
		return etniaTraits;
	}

	protected override Mood face(Trait a, Trait b)
	{
		var etnA = a as EtniaTrait;
		var etnB = a as EtniaTrait;
		if(etnA == null || etnB == null){
			throw new UnityException("Facing Invalid Etnia Traits");
		}

		//TODO implement method to face complexion trait
		return Mood.SCARED;
	}
}

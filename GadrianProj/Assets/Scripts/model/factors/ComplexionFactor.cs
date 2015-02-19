using UnityEngine;
using System.Collections.Generic;

public class ComplexionFactor : PersonalityFactor
{
	public static ComplexionTrait SMALL;
	public static ComplexionTrait FAT;
	public static ComplexionTrait TALL;
	public static ComplexionTrait AVERAGE;
	[SerializeField]
	private Sprite smallSprite;
	[SerializeField]
	private Sprite fatSprite;
	[SerializeField]
	private Sprite tallSprite;
	[SerializeField]
	private Sprite averageSprite;

	private readonly static List<Trait> etniaTraits = new List<Trait>();

	private void Awake ()
	{
		SMALL = new ComplexionTrait ( smallSprite );
		FAT = new ComplexionTrait ( fatSprite );
		TALL = new ComplexionTrait ( tallSprite );
		AVERAGE = new ComplexionTrait ( averageSprite );

		etniaTraits.Add(SMALL);
		etniaTraits.Add(FAT);
		etniaTraits.Add(TALL);
		etniaTraits.Add(AVERAGE);
	}

	public override List<Trait> getTraits()
	{
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

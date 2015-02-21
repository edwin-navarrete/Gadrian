using UnityEngine;
using System.Collections.Generic;

public class ComplexionFactor : PersonalityFactor
{
	public static ComplexionTrait SMALL;
	public static ComplexionTrait FAT;
	public static ComplexionTrait TALL;
	public static ComplexionTrait AVERAGE;
	[SerializeField]
	private Sprite smallBody;
	[SerializeField]
	private Sprite smallComplexion;
	[SerializeField]
	private Sprite fatBody;
	[SerializeField]
	private Sprite fatComplexion;
	[SerializeField]
	private Sprite tallBody;
	[SerializeField]
	private Sprite tallComplexion;
	[SerializeField]
	private Sprite averageBody;
	[SerializeField]
	private Sprite averageComplexion;

	private readonly static List<Trait> complexionTraits = new List<Trait>();

	private void Awake ()
	{
		SMALL = new ComplexionTrait ( smallBody, smallComplexion );
		FAT = new ComplexionTrait ( fatBody, fatComplexion );
		TALL = new ComplexionTrait ( tallBody, tallComplexion );
		AVERAGE = new ComplexionTrait ( averageBody, averageComplexion );

		complexionTraits.Add(SMALL);
		complexionTraits.Add(FAT);
		complexionTraits.Add(TALL);
		complexionTraits.Add(AVERAGE);
	}

	public override List<Trait> getTraits()
	{
		return complexionTraits;
	}

	protected override Mood face(Trait a, Trait b)
	{
		var etnA = a as ComplexionTrait;
		var etnB = a as ComplexionTrait;
		if(etnA == null || etnB == null){
			throw new UnityException("Facing Invalid Etnia Traits");
		}

		//TODO implement method to face complexion trait
		return Mood.SCARED;
	}
}

using UnityEngine;
using System.Collections.Generic;

public class ComplexionFactor : PersonalityFactor
{
	public static ComplexionTrait SMALL;
	public static ComplexionTrait FAT;
	public static ComplexionTrait FURRY;

	[SerializeField]
	private Sprite smallBody;
	[SerializeField]
	private Sprite smallComplexion;
	[SerializeField]
	private Sprite fatBody;
	[SerializeField]
	private Sprite fatComplexion;
	[SerializeField]
	private Sprite furryBody;
	[SerializeField]
	private Sprite furryComplexion;

	[SerializeField]
	private RectTransform smallFacePosition;
	[SerializeField]
	private RectTransform fatFacePosition;
	[SerializeField]
	private RectTransform furryFacePosition;

	[SerializeField]
	private Sprite uiSmallBody;
	[SerializeField]
	private Sprite uiSmallComplexion;
	[SerializeField]
	private Sprite uiFatBody;
	[SerializeField]
	private Sprite uiFatComplexion;
	[SerializeField]
	private Sprite uiFurryBody;
	[SerializeField]
	private Sprite uiFurryComplexion;


	private readonly static List<Trait> complexionTraits = new List<Trait>();

	private void Awake ()
	{
		SMALL = new ComplexionTrait ( smallBody, smallComplexion, uiSmallBody, uiSmallComplexion, smallFacePosition );
		FAT = new ComplexionTrait ( fatBody, fatComplexion, uiFatBody, uiFatComplexion, fatFacePosition );
		FURRY = new ComplexionTrait ( furryBody, furryComplexion, uiFurryBody, uiFurryComplexion, furryFacePosition );

		complexionTraits.Add(SMALL);
		complexionTraits.Add(FAT);
		complexionTraits.Add(FURRY);

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
			throw new UnityException("Facing Invalid Complexion Traits");
		}

		if(a ==  ComplexionFactor.FAT && b == ComplexionFactor.SMALL) 	
			return Mood.ANGRY;

		if(a ==  ComplexionFactor.FURRY && b == ComplexionFactor.SMALL) 	
			return Mood.INDIFERENT;

		return Mood.SCARED;
	}
}

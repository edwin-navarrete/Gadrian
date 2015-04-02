using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeadAccessoryFactor : PersonalityFactor
{
	public static HeadAccessoryTrait HORN;
	public static HeadAccessoryTrait HAIR;
	public static HeadAccessoryTrait BALD;

	[SerializeField]
	private Sprite hornSprite;
	[SerializeField]
	private Sprite hairSprite;

	[SerializeField]
	private Sprite uiHornSprite;
	[SerializeField]
	private Sprite uiHairSprite;

	[SerializeField]
	private Transform headAccessoryTransform;

	private readonly static List<Trait> headAccessoryTraits = new List<Trait> ();

	public void Awake ()
	{
		HORN = new HeadAccessoryTrait ( hornSprite, uiHornSprite );
		HAIR = new HeadAccessoryTrait ( hairSprite, uiHairSprite );
		BALD = new HeadAccessoryTrait ();

		headAccessoryTraits.Add ( HORN );
		headAccessoryTraits.Add ( HAIR );
		headAccessoryTraits.Add ( BALD );
	}

	public override List<Trait> getTraits ()
	{
		return headAccessoryTraits;
	}

	protected override Mood face (Trait a, Trait b)
	{
		var etnA = a as HeadAccessoryTrait;
		var etnB = a as HeadAccessoryTrait;
		if ( etnA == null || etnB == null )
		{
			throw new UnityException ( "Facing Invalid HeadAccessory Traits" );
		}

		if ( a == HeadAccessoryFactor.BALD && b == HeadAccessoryFactor.HAIR )
			return Mood.ANGRY;

		if ( a == HeadAccessoryFactor.HORN && b == HeadAccessoryFactor.HAIR )
			return Mood.INDIFERENT;

		return Mood.SCARED;
	}
}

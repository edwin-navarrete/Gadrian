using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EarAccessoryFactor : PersonalityFactor
{
    public static EarAccessoryTrait POINTY;
    public static EarAccessoryTrait FURRY;
    public static EarAccessoryTrait NORMAL;

    [SerializeField]
    private Sprite pointyEarSprite;
    [SerializeField]
    private Sprite furryEarSprite;
    [SerializeField]
    private Sprite normalEarSprite;

    //	[SerializeField]
    //	private Transform headAccessoryTransform;

    private readonly List<Trait> headAccessoryTraits = new List<Trait>();

    public void Awake ()
    {
        if ( POINTY == null )
            POINTY = new EarAccessoryTrait( pointyEarSprite );
        if ( FURRY == null )
            FURRY = new EarAccessoryTrait( furryEarSprite );
        if ( NORMAL == null )
            NORMAL = new EarAccessoryTrait( normalEarSprite );

        headAccessoryTraits.Add( POINTY );
        headAccessoryTraits.Add( FURRY );
        headAccessoryTraits.Add( NORMAL );
    }

    public override List<Trait> getTraits ()
    {
        return headAccessoryTraits;
    }

    protected override Mood face (Trait a, Trait b)
    {
        var etnA = a as EarAccessoryTrait;
        var etnB = a as EarAccessoryTrait;
        if ( etnA == null || etnB == null )
        {
            throw new UnityException( "Facing Invalid HeadAccessory Traits" );
        }

        if ( a == EarAccessoryFactor.NORMAL && b == EarAccessoryFactor.FURRY )
            return Mood.ANGRY;

        if ( a == EarAccessoryFactor.POINTY && b == EarAccessoryFactor.FURRY )
            return Mood.INDIFERENT;

        return Mood.SCARED;
    }
}

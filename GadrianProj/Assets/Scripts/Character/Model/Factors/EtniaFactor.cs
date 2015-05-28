using UnityEngine;
using System;
using System.Collections.Generic;

/**
 * Represents the color of skin as a trait
 * */
public class EtniaFactor : PersonalityFactor
{
    public static EtniaTrait BROWN;
    public static EtniaTrait RED;
    public static EtniaTrait GOLD;

    [SerializeField]
    private int brown;
    [SerializeField]
    private int red;
    [SerializeField]
    private int gold;

    private readonly List<Trait> etniaTraits = new List<Trait>();

    private void Awake ()
    {
        if ( BROWN == null )
            BROWN = new EtniaTrait( brown );
        if ( RED == null )
            RED = new EtniaTrait( red );
        if ( GOLD == null )
            GOLD = new EtniaTrait( gold );

        etniaTraits.Add( BROWN );
        etniaTraits.Add( RED );
        etniaTraits.Add( GOLD );
    }

    public override List<Trait> getTraits ()
    {
        return etniaTraits;
    }

    protected override Mood face (Trait a, Trait b)
    {
        var etnA = a as EtniaTrait;
        var etnB = a as EtniaTrait;
        if ( etnA == null || etnB == null )
        {
            throw new UnityException( "Facing Invalid Etnia Traits" );
        }

        // LEMON fears YELLOW
        // GOLD fears LEMON
        if ( RED.Equals( etnA ) && BROWN.Equals( etnB )
           || GOLD.Equals( etnA ) && RED.Equals( etnB ) )
        {
            return Mood.SCARED;
        }

        if ( GOLD.Equals( etnA ) && BROWN.Equals( etnB ) )
        {
            return Mood.INDIFERENT;
        }

        // LEMON hates GOLD
        // YELLOW hates LEMON
        // YELLOW hates GOLD
        // GOLD hates YELLOW
        return Mood.ANGRY;
    }

}


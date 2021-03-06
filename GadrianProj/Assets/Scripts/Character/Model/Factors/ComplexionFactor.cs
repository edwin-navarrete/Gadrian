﻿using UnityEngine;
using System.Collections.Generic;

public class ComplexionFactor : PersonalityFactor
{
    public static ComplexionTrait SMALL;
    public static ComplexionTrait FAT;
    public static ComplexionTrait TALL;

    [SerializeField]
    private int small;
    [SerializeField]
    private int fat;
    [SerializeField]
    private int tall;

    private readonly List<Trait> complexionTraits = new List<Trait>();

    private void Awake ()
    {
        if ( SMALL == null )
            SMALL = new ComplexionTrait( small );
        if ( FAT == null )
            FAT = new ComplexionTrait( fat );
        if ( TALL == null )
            TALL = new ComplexionTrait( tall );

        complexionTraits.Add( SMALL );
        complexionTraits.Add( FAT );
        complexionTraits.Add( TALL );

    }

    public override List<Trait> getTraits ()
    {
        return complexionTraits;
    }

    protected override Mood face (Trait a, Trait b)
    {
        var etnA = a as ComplexionTrait;
        var etnB = a as ComplexionTrait;
        if ( etnA == null || etnB == null )
        {
            throw new UnityException( "Facing Invalid Complexion Traits" );
        }

        if ( a == ComplexionFactor.FAT && b == ComplexionFactor.SMALL )
            return Mood.ANGRY;

        if ( a == ComplexionFactor.TALL && b == ComplexionFactor.SMALL )
            return Mood.INDIFERENT;

        return Mood.SCARED;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class ComplexionFactor : PersonalityFactor
{
    public static ComplexionTrait SMALL;
    public static ComplexionTrait FAT;
    public static ComplexionTrait TALL;

    [SerializeField]
    private RectTransform smallFacePosition;
    [SerializeField]
    private RectTransform fatFacePosition;
    [SerializeField]
    private RectTransform furryFacePosition;
    
    private readonly static List<Trait> complexionTraits = new List<Trait>();

    private void Awake ()
    {
        SMALL = new ComplexionTrait( smallFacePosition );
        FAT = new ComplexionTrait( fatFacePosition );
        TALL = new ComplexionTrait( furryFacePosition );

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

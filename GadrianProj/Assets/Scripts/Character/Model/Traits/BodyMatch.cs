using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BodyMatch : MonoBehaviour
{
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject face;

    public Sprite[] bodySprites;

    private ComplexionTrait complexion;
    private EtniaTrait etnia;

    public int ComplexionValue { get; private set; }

    public int EtniaValue { get; private set; }

    public ComplexionTrait Complexion
    {
        set
        {
            complexion = value;
            if ( etnia != null )
            {
                SetBodyMatch();
            }
        }
    }

    public EtniaTrait Etnia
    {
        set
        {
            etnia = value;
            if ( complexion != null )
            {
                SetBodyMatch();
            }
        }
    }

    private void SetBodyMatch ()
    {
        Sprite bodySprite = LookForBodySprite();

        SpriteRenderer characterBody = body.GetComponent<SpriteRenderer>();
        if ( characterBody == null )
        {
            Image characterSpriteBody = body.GetComponent<Image>();
            characterSpriteBody.sprite = bodySprite;
        }
        else
        {
            characterBody.sprite = bodySprite;
        }
    }

    private Sprite LookForBodySprite ()
    {
        if ( complexion == ComplexionFactor.SMALL )
            ComplexionValue = 1;

        if ( complexion == ComplexionFactor.FAT )
            ComplexionValue = 2;

        if ( complexion == ComplexionFactor.TALL )
            ComplexionValue = 3;

        if ( etnia == EtniaFactor.GOLD )
            EtniaValue = 1;

        if ( etnia == EtniaFactor.RED )
            EtniaValue = 2;

        if ( etnia == EtniaFactor.BROWN )
            EtniaValue = 3;

        int index = ComplexionValue * EtniaValue - 1;

        try
        {
            return bodySprites[index];
        }
        catch ( System.IndexOutOfRangeException )
        {
            Debug.LogErrorFormat( "Error trying to access Sprite from bodySprites in BodyMatch with index {0}", index );
        }
        return null;
    }
}

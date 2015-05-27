using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BodyMatch : MonoBehaviour
{
    [SerializeField]
    private GameObject body;

    public Sprite[] bodySprites;

    private int? complexion;
    private int? etnia;

    public int? Complexion
    {
        set
        {
            complexion = value;
            if ( etnia != null )
            {
                SetBodyMatch();
            }
        }

        private get
        {
            return complexion;
        }
    }

    public int? Etnia
    {
        set
        {
            etnia = value;
            if ( complexion != null )
            {
                SetBodyMatch();
            }
        }

        private get
        {
            return etnia;
        }
    }

    private void SetBodyMatch ()
    {
        Sprite bodySprite = LookForBodySprite();

        SpriteRenderer characterBody = body.GetComponent<SpriteRenderer>();
        if ( characterBody != null )
        {
            characterBody.sprite = bodySprite;
        }
    }

    private Sprite LookForBodySprite ()
    {
        int index = (int) ( Complexion + 3 * Etnia );

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

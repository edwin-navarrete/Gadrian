using UnityEngine;
using System.Collections.Generic;

public class PlayerOverTile : MonoBehaviour
{
    [SerializeField]
    private Sprite nonSolidTile;
    [SerializeField]
    private Sprite solidTile;

    private SpriteRenderer renderSprite;

    public void Awake ()
    {
        renderSprite = GetComponent<SpriteRenderer>();
    }

    public void SolidifyTile ()
    {
        renderSprite.sprite = solidTile;
    }

    public void UnsolidifyTile ()
    {
        renderSprite.sprite = nonSolidTile;
    }

    public bool IsTileSolid ()
    {
        if ( renderSprite.sprite.Equals(solidTile) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
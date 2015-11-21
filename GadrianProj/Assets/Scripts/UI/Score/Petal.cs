using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Gadrians.Util;

[RequireComponent(typeof(Image))]
public class Petal : MonoBehaviour
{
    private LeafState state;
    private Image petal; 

    public LeafState State
    {
        get
        {
            return state;
        }
    }

    public float FadePercent
    {
        get
        {
            float percent = petal.color.a;
            return percent;
        }
    }

    private void Start()
    {
        petal = GetComponent<Image>();
        state = LeafState.Opened;
    }

    public void FadePetal(float fadePercent)
    {
        petal.CrossFadeAlpha( fadePercent, 1.0f, true );
        if ( fadePercent == 0.5f )
        {
            state = LeafState.Half;
        }
        else if ( fadePercent == 0.0f )
        {
            state = LeafState.Closed;
        }
    }
}

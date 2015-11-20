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

    private void Start()
    {
        petal = GetComponent<Image>();
        state = LeafState.Opened;
    }

    public void FadePetal(int action)
    {
        petal.CrossFadeAlpha( 0.0f, 1.0f, true );
        state = LeafState.Closed;
    }
}

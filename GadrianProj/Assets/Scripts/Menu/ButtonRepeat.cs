using UnityEngine;
using System.Collections;

public class ButtonRepeat : MonoBehaviour
{
    public void LoadSameLevel ()
    {
        Application.LoadLevel( Application.loadedLevel );
    }
}

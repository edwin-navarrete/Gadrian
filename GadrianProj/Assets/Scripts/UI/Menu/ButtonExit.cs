using UnityEngine;
using System.Collections;

public class ButtonExit : MonoBehaviour
{
    public void ExitGame ()
    {
        PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
        Application.LoadLevel( Application.loadedLevel );
#else
        Application.Quit();
#endif
    }
}

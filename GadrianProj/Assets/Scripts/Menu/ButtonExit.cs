using UnityEngine;
using System.Collections;

public class ButtonExit : MonoBehaviour
{
    public void ExitGame ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
#endif
#if UNITY_ANDROID
        Application.Quit();
#endif
    }
}

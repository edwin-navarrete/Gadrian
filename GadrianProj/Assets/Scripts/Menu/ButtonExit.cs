using UnityEngine;
using System.Collections;

public class ButtonExit : MonoBehaviour
{
    public void ExitGame ()
    {
        PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
#else
        Application.Quit();
#endif
    }
}

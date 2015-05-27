using UnityEngine;
using System.Collections;

public class ButtonNextLevel : MonoBehaviour
{
    public void LoadNextLevel ()
    {
        int levelToLoad = PlayerPrefs.GetInt( Strings.LevelToLoad );
        PlayerPrefs.SetInt( Strings.LevelToLoad, levelToLoad );
        Application.LoadLevel( Application.loadedLevel );
    }
}

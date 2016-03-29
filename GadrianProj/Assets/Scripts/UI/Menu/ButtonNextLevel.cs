using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonNextLevel : MonoBehaviour
{
    private Button buttonNextLevel;

    public void Awake ()
    {
        buttonNextLevel = GetComponent<Button>();
    }

    public void Start ()
    {
        if ( !IsThereANextLevel() )
        {
            buttonNextLevel.interactable = false;
        }
    }

    private bool IsThereANextLevel ()
    {
        int levelToLoad = PlayerPrefs.GetInt( Strings.LevelToLoad, 0 );
        string fileName = string.Format( Strings.GenericLevelName, ++levelToLoad );
        if ( Resources.Load<Level>( Strings.LevelPath + fileName ) != null)
        {
            return true;
        }
        else
        {
            PlayerPrefs.SetInt(Strings.LevelToLoad, 0);
            return false;
        }
    }

    
    public void LoadNextLevel ()
    {
        int levelToLoad = PlayerPrefs.GetInt( Strings.LevelToLoad, 0 );
        PlayerPrefs.SetInt( Strings.LevelToLoad, ++levelToLoad );
        Application.LoadLevel( Application.loadedLevel );
    }
}

using UnityEngine;
using System.Collections;

public class EndGameNotification : MonoBehaviour
{
    public void Start()
    {
        CharacterManager.Instance.Won += PopUp;
        gameObject.SetActive( false );
    }

    public void PopUp()
    {
        gameObject.SetActive( true );
    }
}

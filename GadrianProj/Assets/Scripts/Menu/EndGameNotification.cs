using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameNotification : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        CharacterManager.Instance.Won += PopUp;
        animator = GetComponent<Animator>();
    }

    public void PopUp()
    {
        animator.SetTrigger( "PopUp" );
    }
}

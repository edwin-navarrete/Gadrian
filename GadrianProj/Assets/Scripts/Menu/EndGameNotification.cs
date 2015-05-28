using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameNotification : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnEnable ()
    {
        EventManager.StartListening( Events.Won, PopUp );
    }

    public void OnDisable ()
    {
        EventManager.StopListening( Events.Won, PopUp );
    }

    public void PopUp()
    {
        animator.SetTrigger( "PopUp" );
    }
}

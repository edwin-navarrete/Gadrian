using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameNotification : MonoBehaviour
{
    [SerializeField]
    private Button nextLevelButton;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        EventManager.StartListening( Events.Won, WinPopUp );
        EventManager.StartListening( Events.Loss, LossPopUp );
    }

    public void OnDisable()
    {
        EventManager.StopListening( Events.Won, WinPopUp );
        EventManager.StopListening( Events.Loss, LossPopUp );
    }

    private void WinPopUp()
    {
        nextLevelButton.interactable = true;
        animator.SetTrigger( "PopUp" );
    }

    private void LossPopUp()
    {
        nextLevelButton.interactable = false;
        animator.SetTrigger( "PopUp" );
    }
}

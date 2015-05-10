using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterRepresentation : MonoBehaviour
{
	[SerializeField]
	private Image body;
	[SerializeField]
	private Image complexion;
	private CharacterManager manager;

    private void Awake ()
    {
        TurnOff();
        manager = CharacterManager.Instance;
    }

    #region OnEnable/OnDisable

    private void OnEnable ()
    {
        manager.StartingDrag += TurnOn;
        manager.FinishingDrag += TurnOff;
    }

    private void OnDisable ()
    {
        manager.StartingDrag -= TurnOn;
        manager.FinishingDrag -= TurnOff;
    }

    #endregion

    private void TurnOn ()
    {
        this.body.enabled = true;
        this.complexion.enabled = true;
        this.body.sprite = body.sprite;
        this.complexion.sprite = complexion.sprite;
    }

    private void TurnOff ()
    {
        this.transform.position = new Vector2( 0, Screen.height + 100 );
        this.body.sprite = null;
        this.complexion.sprite = null;
        this.body.enabled = false;
        this.complexion.enabled = false;
    }
}

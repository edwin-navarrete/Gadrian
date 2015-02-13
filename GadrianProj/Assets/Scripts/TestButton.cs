using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TestButton : MonoBehaviour, IPointerClickHandler
{
	public InputField xInput;
	public InputField yInput;
	public MoodHandler mHandler;

	#region Miembros de IPointerClickHandler

	public void OnPointerClick (PointerEventData eventData)
	{
		Vector2 point = new Vector2 ( float.Parse ( xInput.text, System.Globalization.NumberStyles.Float ),
			float.Parse ( yInput.text, System.Globalization.NumberStyles.Float ) );
		mHandler.SetMood ( new Mood ( point ) );
	}

	#endregion
}

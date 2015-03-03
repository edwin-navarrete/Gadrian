using UnityEngine;
using System.Collections;

public class LayerController : MonoBehaviour
{
	[SerializeField]
	private LayerMask gridLayer;
	[SerializeField]
	private LayerMask ignoreRaycastLayer;

	public void Start ()
	{
		CharacterManager.Instance.StartingDrag += SetLayerToGrid;
		CharacterManager.Instance.FinishedDrag += SetLayerToIgnore;

		SnapCharacter.MovingCharacter += SetLayerToGrid;
		SnapCharacter.MovedCharacter += SetLayerToIgnore;

		Debug.Log ( "gridLayerValue:" + gridLayer.value );
		Debug.Log ( "ignoreLayerValue:" + ignoreRaycastLayer.value );
		//FIXME layers values are incorrect from editor, find a way to fix it
	}

	private void SetLayerToIgnore ()
	{
		gameObject.layer = 2;
	}

	private void SetLayerToGrid (Sprite arg0, Sprite arg1)
	{
		SetLayerToGrid ();
	}

	private void SetLayerToGrid ()
	{
		gameObject.layer = 8;
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D) )]
public class ClickDebugger : MonoBehaviour
{

	public void OnMouseDown ()
	{
		Debug.Log ( "Click on:" + gameObject.name );
	}
}

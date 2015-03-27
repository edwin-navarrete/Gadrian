using UnityEngine;
using System.Collections;

public class ShutDown : MonoBehaviour
{
	private GameObject child;
	private GameObject[] grandChilds;
	private int grandChildsAmount;

	public void Start ()
	{
		child = transform.GetChild( 0 ).gameObject;
	}

	public void StartCheck ()
	{
		grandChildsAmount = child.transform.childCount;
		grandChilds = new GameObject[grandChildsAmount];
		for ( int i = 0; i < grandChildsAmount; i++ )
		{
			grandChilds[i] = child.transform.GetChild ( i ).gameObject;
		}
		StartCoroutine ( CheckChilds () );
	}

	private IEnumerator CheckChilds ()
	{
		int activeChilds = 0;
		bool isOver = false;
		while ( !isOver )
		{
			foreach ( GameObject go in grandChilds )
			{
				if ( go.activeInHierarchy )
					activeChilds++;
			}
			if ( activeChilds == 0 )
			{
				Debug.Log ( "Childs no more" );
				isOver = true;
				gameObject.SetActive ( false );
				CharacterManager.Instance.FinishCharacterPlacement ();
			}
			activeChilds = 0;
			yield return null;
		}
	}
}

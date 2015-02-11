using UnityEngine;
using System;
using System.Collections;

/**
 * Represents a particular etnia by setting the skin color 
 **/
public class EtniaTrait : MonoBehaviour, ITrait, IEquatable<EtniaTrait>
{
	Color skinColor;

	EtniaTrait(Color skinColor){
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}


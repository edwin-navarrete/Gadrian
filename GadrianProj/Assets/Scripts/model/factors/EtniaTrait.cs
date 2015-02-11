using UnityEngine;
using System;
using System.Collections;

/**
 * Represents a particular etnia by setting the skin color 
 **/
public class EtniaTrait : MonoBehaviour, ITrait, IEquatable<EtniaTrait>
{
	Color skinColor;

	public EtniaTrait(Color skinColor){
	
	}

	public bool Equals(EtniaTrait other){
		if(other == null)
			return false;
		return skinColor.Equals(other.skinColor);
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


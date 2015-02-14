using UnityEngine;
using System.Collections;

/**
 * Is a specific set of personality factors that will be used to represent
 * the personality of each character in the level
 * */
public class PersonalityModel : MonoBehaviour {

	IList factors;
	Personality personality;

	// Load or calculates the list of factors to represent the personality of characters 
	void CalculateFactors () {
		foreach ( var factor in factors )
		{
			IPersonalityFactor factorToTrait = factor as IPersonalityFactor;
			personality.Traits = factorToTrait.GetRandomTrait ();	// Fill Traits list of the personality of this caracter
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections.Generic;

/**
 * Is a specific set of personality factors that will be used to represent
 * the personality of each character in the level
 * */
public class PersonalityModel : MonoBehaviour {

	List<PersonalityFactor> factors;

	public List<PersonalityFactor> Factors
	{
		get
		{
			return factors;
		}
	}

	Personality personality;

	// Load or calculates the list of factors to represent the personality of characters 
	public void CalculateFactors () {
		foreach ( var factor in factors )
		{
			PersonalityFactor factorToTrait = factor as PersonalityFactor;
			ITrait trait = factorToTrait.GetRandomTrait ();	// Fill Traits list of the personality of this caracter
			trait.AffectCharacter ( gameObject );
			personality += trait;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

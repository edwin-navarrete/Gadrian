using UnityEngine;
using System.Collections;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality {

	IList traits;

	// based on affinity with the other, calculates the corresponding emotional state based on distance 
	Mood sense(Personality other, float distance){
		//FIXME implement method
		return Mood.INDIFERENT;
	}
}

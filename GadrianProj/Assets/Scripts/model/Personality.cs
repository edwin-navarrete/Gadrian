using UnityEngine;
using System.Collections.Generic;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality {

	List<ITrait> traits = new List<ITrait>();
	PersonalityModel model;

	// Is it possible to add a trait to the personality
	public static Personality operator +(Personality m1, ITrait m2) 
	{
		m1.traits.Add(m2);
		return m1;
	}

	// based on affinity with the other, calculates the corresponding emotional state based on distance 
	Mood sense(Personality other, float distance){
		Mood finalMood = Mood.INDIFERENT; 
		if(other.traits.Count != this.traits.Count){
			Debug.LogError("Incompatible personalities!");
			return finalMood;
		}

		for(int i =0; i < other.traits.Count; i++){
			ITrait mine = traits[i];
			ITrait his = other.traits[i];
			//FIXME 
			finalMood += model.Factors[i].confront(mine, his);
		}
		return finalMood;
	}
}

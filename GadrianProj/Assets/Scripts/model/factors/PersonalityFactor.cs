using UnityEngine;
using System.Collections;

/**
 * Represents one of the aspects that conforms the personality of each character.
 * e.g. Musical Taste
 *
 * */
public abstract class PersonalityFactor  {

	// Returns the mood resulting of putting together a character with a trait 'a' with another a trait 'b'
	// The traits must be of the same factor
	public Mood confront(ITrait a, ITrait b){
		if(a.Equals(b))
			return Mood.HAPPY;
		return face(a,b);
	}

	// Returns a ramdon value trait for the T factor, e.g. For EtniaFactor might return: YELLOW, LEMON, GOLD
	public abstract ITrait GetRandomTrait ();

	// Returns a ramdon value trait for the T factor, e.g. For EtniaFactor might return: YELLOW, LEMON, GOLD
	protected abstract Mood face(ITrait a, ITrait b);

	// Use if Traits are going to be MonoBehaviours
	//public void AddTraitComponent (GameObject character);
}

using UnityEngine;
using System.Collections.Generic;

/**
 * Represents one of the aspects that conforms the personality of each character.
 * e.g. Musical Taste
 *
 * */
public abstract class PersonalityFactor : MonoBehaviour
{
	// Returns the mood resulting of putting together a character with a trait 'a' with another a trait 'b'
	// The traits must be of the same factor
	public Mood confront(Trait a, Trait b){
		if(a.Equals(b))
			return Mood.HAPPY;
		Mood ret = face(a,b);
		if( Mood.SAD.Equals(ret) )
			throw new UnityException("Factor should never return SAD or HAPPY");
		return ret;
	}

	//Returns the list of traits that makes unique this personality
	public abstract List<Trait> getTraits();

	// Face returns the energetic mood (ANGRY, HAPPY, SCARED) resulting of facing trait a with b
	protected abstract Mood face(Trait a, Trait b);


	// Use if Traits are going to be MonoBehaviours
	//public void AddTraitComponent (GameObject character);

    public void OnDestory()
    {
        List<Trait> traits = getTraits();
        traits.Clear();
    }
}

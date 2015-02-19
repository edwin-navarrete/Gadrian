using UnityEngine;
using System.Collections.Generic;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality : MonoBehaviour
{
	List<Trait> traits = new List<Trait>();
	PersonalityModel model;

	// Is it possible to add a trait to the personality
	public static Personality operator +(Personality m1, Trait m2) 
	{
		m1.traits.Add(m2);
		return m1;
	}

	private void SetTraitList (List<Trait> traits)
	{
		if ( traits.Count == 0 )
			Debug.LogError ( "The list of traits is empty" );
		else
			this.traits = traits;
	}

	public void TraitsEffect ()
	{
		foreach ( Trait trait in traits )
		{
			trait.AffectCharacter ( gameObject );
		}
	}

	public void SetupPersonality (PersonalityModel model)
	{
		this.model = model;
		SetTraitList ( model.getPersonalityTraits (UnityEngine.Random.Range ( 0, model.PersonalityCnt ) ) );
	}

	// based on affinity with the other, calculates the corresponding emotional state based on distance 
	//FIXME this method is not currently using the distance parameter, and requires to used only with 
	// immediate neighbours
	Mood sense(Personality other, float distance){
		Mood finalMood = Mood.INDIFERENT; 
		if(other.traits.Count != this.traits.Count){
			Debug.LogError("Incompatible personalities!");
			return finalMood;
		}
		List<Trait>.Enumerator loop1 = traits.GetEnumerator();
		List<Trait>.Enumerator loop2 = other.traits.GetEnumerator();
		HashSet<PersonalityFactor>.Enumerator modelLoop = model.Factors.GetEnumerator();

		while(modelLoop.MoveNext() & loop1.MoveNext() && loop2.MoveNext()){
			Trait mine = loop1.Current;
			Trait his = loop2.Current;
			finalMood += modelLoop.Current.confront(mine, his);
		}
		return finalMood;
	}
}

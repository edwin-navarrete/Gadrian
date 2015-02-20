using UnityEngine;
using System.Collections.Generic;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality : MonoBehaviour
{
	private List<Trait> traits = new List<Trait>();
	private PersonalityModel model;
	private MoodHandler mooodHandler;
	private SnapCharacter snapCharacter;
	private NeighbourSensor sensor;

	private void Awake ()
	{
		mooodHandler = GetComponent<MoodHandler> ();
		snapCharacter = GetComponent<SnapCharacter> ();
		sensor = GetComponentInChildren<NeighbourSensor> ();
	}

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

	public void CopyPersonality (Personality personalityToCopy)
	{
		SetTraitList ( personalityToCopy.traits );
		model = personalityToCopy.model;
	}

	public void RefreshMood ()
	{
		Mood mood = new Mood ();
		List<Personality> neighbours = sensor.Neighbours;
		foreach ( Personality neighbour in neighbours )
		{
			mood = sense ( neighbour, CharacterManager.Instance.AskGridPosition ( neighbour.transform.position ) );
		}

		mooodHandler.SetNextMood ( mood );
	}

	// based on affinity with the other, calculates the corresponding emotional state based on distance 
	//FIXME this method is not currently using the distance parameter, and requires to used only with 
	// immediate neighbours
	private Mood sense(Personality other, Vector3 otherPosition)
	{
		Vector3 gridPosition = CharacterManager.Instance.AskGridPosition ( transform.position );
		if ( Vector3.Distance ( otherPosition, gridPosition ) < 1.1f 
			&& Vector3.Distance ( otherPosition, gridPosition ) > 0.1f )
		{
			return Mood.INDIFERENT; //	It's being compared to non-neighbour or himself
		}

		Mood finalMood = Mood.INDIFERENT; 
		if(other.traits.Count != this.traits.Count){
			Debug.LogError("Incompatible personalities!");
			mooodHandler.SetNextMood ( finalMood );
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

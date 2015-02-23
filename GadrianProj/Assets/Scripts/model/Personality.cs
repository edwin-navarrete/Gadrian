using UnityEngine;
using System.Collections.Generic;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality : MonoBehaviour
{
	private PersonalityModel model;
	private MoodHandler mooodHandler;
	private SnapCharacter snapCharacter;
	private Animator animator;

	private List<Trait> traits = new List<Trait> ();
	private List<Personality> neighbours;

	private UnityEngine.Events.UnityAction Refresh;
	public UnityEngine.Events.UnityAction SetInitialMood;

	public void Awake ()
	{
		mooodHandler = GetComponent<MoodHandler> ();
		snapCharacter = GetComponent<SnapCharacter> ();
		animator = GetComponentInChildren<Animator> ();

		SetInitialMood = () =>
		{
			RefreshNeighbourList ();
			RefreshNeighbourMood ();
			RefreshMood ();
		};

		Refresh = () =>
		{
			RefreshNeighbourMood ();
			RefreshMood ();
			RefreshNeighbourList ();
			RefreshNeighbourMood ();
		};
	}

	public void OnEnable ()
	{
		if (snapCharacter != null )
			snapCharacter.Movement += Refresh;
	}

	public void OnDisable ()
	{
		if ( snapCharacter != null )
			snapCharacter.Movement -= Refresh;
	}

	// Is it possible to add a trait to the personality
	public static Personality operator +(Personality m1, Trait m2) 
	{
		m1.traits.Add(m2);
		return m1;
	}

	#region Trait manipulation

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

	#endregion

	#region Personality initialization

	public void SetupPersonality (PersonalityModel model, int personalityIdx)
	{
		if ( model == null )
		{
			Debug.Log ( "model is null" );
		}
		this.model = model;
		SetTraitList ( model.getPersonalityTraits ( personalityIdx ) );
	}

	public void CopyPersonality (Personality personalityToCopy)
	{
		SetTraitList ( personalityToCopy.traits );
		model = personalityToCopy.model;
	}

	#endregion

	#region Mood refresh Personal/Neighbour

	private void RefreshMood ()
	{
		Debug.Log("Started Sensing for:"+this.transform.position);
		Mood mood = new Mood ();
		foreach ( Personality neighbour in neighbours )
		{
			mood += sense ( neighbour, CharacterManager.Instance.AskGridPosition ( neighbour.transform.position ) );
			Debug.Log("Sensing:"+neighbour.transform.position+">"+mood.getFeel());
		}
		mooodHandler.SetNextMood ( mood );
		UpdateMoodAnimation ( mood );
	}

	private void RefreshNeighbourList ()
	{
		neighbours = CharacterManager.Instance.GetNeighbourPersonalities ( transform.position );
		Debug.Log("Refreshed N for:"+transform.position+":"+neighbours.Count);
	}

	private void RefreshNeighbourMood ()
	{
		foreach ( Personality neighbour in neighbours )
		{
			neighbour.RefreshMood ();
		}
	}

	#endregion

	private void UpdateMoodAnimation (Mood mood)
	{
		switch ( mood.getFeel () )
		{
			default:
				break;

			case Mood.Feeling.ANGRY:
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 2 );
				break;

			case Mood.Feeling.HAPPY:
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 1 );
				break;

			case Mood.Feeling.INDIFERENT:
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", -1 );
				break;

			case Mood.Feeling.SAD:
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 0 );
				break;

			case Mood.Feeling.SCARED:
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 3 );
				break;
		}
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

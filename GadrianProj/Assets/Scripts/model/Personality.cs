using UnityEngine;
using System.Collections.Generic;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality : MonoBehaviour
{
	[SerializeField]
	private GameObject body;
	[SerializeField]
	private GameObject complexion;
	[SerializeField]
	private GameObject face;

	private PersonalityModel model;
	private MoodHandler mooodHandler;
	private SnapCharacter snapCharacter;
	private Animator animator;	

	private List<Trait> traits = new List<Trait> ();

	#region Events-Emotions

	public UnityEngine.Events.UnityAction Happy;
	public UnityEngine.Events.UnityAction Sad;
	public UnityEngine.Events.UnityAction Scared;
	public UnityEngine.Events.UnityAction Angry;
	public UnityEngine.Events.UnityAction Indifferent;

	private void OnHappy ()
	{
		if ( Happy != null )
		{
			Happy ();
		}
	}

	private void OnSad ()
	{
		if ( Sad != null )
		{
			Sad ();
		}
	}

	private void OnScared ()
	{
		if ( Scared != null )
		{
			Scared ();
		}
	}

	private void OnAngry ()
	{
		if ( Angry != null )
		{
			Angry ();
		}
	}

	private void OnIndifferent ()
	{
		if ( Indifferent != null )
		{
			Indifferent ();
		}
	}

	#endregion

	public void Awake ()
	{
		mooodHandler = GetComponent<MoodHandler> ();
		snapCharacter = GetComponent<SnapCharacter> ();
		if ( face != null )
			animator = face.GetComponent<Animator> ();
	}

	public void Start ()
	{
		SubscribeEmotions ( face != null );
	}

	private void SubscribeEmotions (bool haveFace)
	{
		if ( haveFace )
		{
			Happy += () =>
			{
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 1 );
			};

			Sad += () =>
			{
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 0 );
			};

			Angry += () =>
			{
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 2 );
			};

			Scared += () =>
			{
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", 3 );
			};

			Indifferent += () =>
			{
				animator.SetTrigger ( "newMood" );
				animator.SetInteger ( "mood", -1 );
			}; 
		}
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
		Debug.LogWarning ( "The list of traits=" + traits);
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

	public void RefreshMood ( List<Personality> neighbours )
	{
		Debug.LogWarning("Started Sensing for:"+CharacterManager.Instance.AskGridPosition ( this.transform.position ));
		Mood mood = new Mood ();
		foreach ( Personality neighbour in neighbours )
		{
			Vector3 n = CharacterManager.Instance.AskGridPosition ( neighbour.transform.position );
			Mood sensed = sense ( neighbour );
			Debug.Log("Sensing:"+n+">"+sensed.getFeel());
			mood += sensed;
		}
		if(mood.getFeel() == Mood.Feeling.PERPLEX)
			mood = Mood.HAPPY;

		if(neighbours.Count == 0)
			mood = Mood.SAD;

		mooodHandler.SetNextMood ( mood );
		UpdateMoodAnimation ( mood );
	}


	#endregion

	private void UpdateMoodAnimation (Mood mood)
	{
		switch ( mood.getFeel () )
		{
			default:
				break;

			case Mood.Feeling.ANGRY:
				OnAngry ();
				break;

			case Mood.Feeling.HAPPY:
				OnHappy ();
				break;

			case Mood.Feeling.INDIFERENT:
				OnIndifferent ();
				break;

			case Mood.Feeling.SAD:
				OnSad ();
				break;

			case Mood.Feeling.SCARED:
				OnScared ();
				break;
		}
	}

	// based on affinity with the other, calculates the corresponding emotional state based on distance 
	//FIXME this method is not currently using the distance parameter, and requires to used only with 
	// immediate neighbours
	private Mood sense(Personality other)
	{
		Mood finalMood = new Mood(); 
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
			Debug.Log("Trais:"+mine + ":"+his);
			finalMood += modelLoop.Current.confront(mine, his);
		}

		return finalMood;
	}
}

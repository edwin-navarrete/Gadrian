﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/**
 * Set of traits that uniquely identifies a single character
 **/

public class Personality : MonoBehaviour
{
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject face;

    private PersonalityModel model;
    private MoodHandler moodHandler;
    private Animator faceAnimator;
    private Animator backgroundAnimator;

    private List<Trait> traits = new List<Trait>();

	public Mood NextMood
	{
		get
		{
			return moodHandler.NextMood;
		}
	}

    public Mood CurrentMood
    {
        get
        {
            return moodHandler.CurrentMood;
        }
    }

    #region Events-Emotions

    public event UnityAction Happy;
    public event UnityAction Sad;
    public event UnityAction Scared;
    public event UnityAction Angry;
    public event UnityAction Indifferent;

    private void OnHappy ()
    {
        if ( Happy != null )
        {
            Happy.Invoke();
        }
    }

    private void OnSad ()
    {
        if ( Sad != null )
        {
            Sad.Invoke();
        }
    }

    private void OnScared ()
    {
        if ( Scared != null )
        {
            Scared.Invoke();
        }
    }

    private void OnAngry ()
    {
        if ( Angry != null )
        {
            Angry.Invoke();
        }
    }

    private void OnIndifferent ()
    {
        if ( Indifferent != null )
        {
            Indifferent.Invoke();
        }
    }

    #endregion

    #region Initialization
    public override string ToString()
    {
        string traitsStr = "";
        foreach (Trait trait in traits)
        {
            if (!System.String.IsNullOrEmpty(traitsStr))
                traitsStr += ",";
            traitsStr += trait;
        }
        return traitsStr;
    }

    public void Awake ()
    {
        moodHandler = GetComponent<MoodHandler>();
        if ( face != null )
        {
            faceAnimator = face.GetComponent<Animator>();
            SubscribeFaceEmotions();
        }
        if ( background != null )
        {
            backgroundAnimator = background.GetComponent<Animator>();
            SubscribeBackgroundEmotions();
        }
    }

    private void SubscribeFaceEmotions ()
    {
        Happy += () =>
        {
            if( !faceAnimator.GetCurrentAnimatorStateInfo(0).IsName("Indifferent") )
                faceAnimator.SetTrigger( "newMood" );
            faceAnimator.SetInteger( "mood", 1 );
            faceAnimator.speed = Random.Range( 0.8f, 1.2f );
        };

        Sad += () =>
        {
            if ( !faceAnimator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Indifferent" ) )
                faceAnimator.SetTrigger( "newMood" );
            faceAnimator.SetInteger( "mood", 0 );
            faceAnimator.speed = Random.Range( 0.8f, 1.2f );
        };

        Angry += () =>
        {
            if ( !faceAnimator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Indifferent" ) )
                faceAnimator.SetTrigger( "newMood" );
            faceAnimator.SetInteger( "mood", 2 );
            faceAnimator.speed = Random.Range( 0.8f, 1.2f );
        };

        Scared += () =>
        {
            if ( !faceAnimator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Indifferent" ) )
                faceAnimator.SetTrigger( "newMood" );
            faceAnimator.SetInteger( "mood", 3 );
            faceAnimator.speed = Random.Range( 0.9f, 1.2f );
        };

        Indifferent += () =>
        {
            if ( !faceAnimator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Indifferent" ) )
                faceAnimator.SetTrigger( "newMood" );
            faceAnimator.SetInteger( "mood", -1 );
            faceAnimator.speed = Random.Range( 0.95f, 1.05f );
        };
    }

    private void SubscribeBackgroundEmotions ()
    {
        Happy += () =>
        {
            backgroundAnimator.SetTrigger( "newMood" );
            backgroundAnimator.SetTrigger( "happy" );
            backgroundAnimator.speed = Random.Range( 0.8f, 1.2f );            
        };

        Sad += () =>
        {
            backgroundAnimator.SetTrigger( "newMood" );
            backgroundAnimator.SetTrigger( "indifferent" );
        };

        Angry += () =>
        {
            backgroundAnimator.SetTrigger( "newMood" );
            backgroundAnimator.SetTrigger( "indifferent" );
        };

        Scared += () =>
        {
            backgroundAnimator.SetTrigger( "newMood" );
            backgroundAnimator.SetTrigger( "indifferent" );
        };

        Indifferent += () =>
        {
            backgroundAnimator.SetTrigger( "newMood" );
            backgroundAnimator.SetTrigger( "indifferent" );
        };
    }

    #endregion

    // Is it possible to add a trait to the personality
    public static Personality operator + (Personality m1, Trait m2)
    {
        m1.traits.Add( m2 );
        return m1;
    }

    #region Trait manipulation

    private void SetTraitList (List<Trait> traits)
    {
        if ( traits.Count == 0 )
            Debug.LogError( "The list of traits is empty" );
        else
            this.traits = traits;
        //Debug.LogWarning ( "The list of traits=" + traits);
    }

    public void TraitsEffect ()
    {
        foreach ( Trait trait in traits )
        {
            trait.AffectCharacter( gameObject );
        }
    }

    #endregion

    #region Personality initialization

    public void SetupPersonality (PersonalityModel model, int personalityIdx)
    {
        if ( model == null )
        {
            Debug.LogError( "model is null" );
        }
        this.model = model;
        SetTraitList( model.getPersonalityTraits( personalityIdx ) );
    }

    public void CopyPersonality (Personality personalityToCopy)
    {
        SetTraitList( personalityToCopy.traits );
        model = personalityToCopy.model;
    }

    #endregion

    #region Mood refresh Personal/Neighbour

    public void RefreshMood (List<Personality> neighbours)
    {
        //Debug.LogWarning("Started Sensing for:"+CharacterManager.Instance.AskGridPosition ( this.transform.position ));
        Mood mood = new Mood();
        foreach ( Personality neighbour in neighbours )
        {
            //Vector3 n = CharacterManager.Instance.AskGridPosition ( neighbour.transform.position );
            Mood sensed = sense( neighbour );
            //Debug.Log("Sensing:"+n+">"+sensed.getFeel());
            mood += sensed;
        }
        if ( mood.getFeel() == Mood.Feeling.PERPLEX )
            mood = Mood.HAPPY;

        if ( neighbours.Count == 0 )
            mood = Mood.SAD;

        moodHandler.SetNextMood( mood );
        UpdateMoodAnimation( mood );
    }


    #endregion

    private void UpdateMoodAnimation (Mood mood)
    {
        switch ( mood.getFeel() )
        {
            default:
                break;

            case Mood.Feeling.ANGRY:
                OnAngry();
                break;

            case Mood.Feeling.HAPPY:
                OnHappy();
                break;

            case Mood.Feeling.INDIFERENT:
                OnIndifferent();
                break;

            case Mood.Feeling.SAD:
                OnSad();
                break;

            case Mood.Feeling.SCARED:
                OnScared();
                break;
        }
    }

    // based on affinity with the other, calculates the corresponding emotional state based on distance 
    //FIXME this method is not currently using the distance parameter, and requires to used only with 
    // immediate neighbours
    private Mood sense (Personality other)
    {
        Mood finalMood = new Mood();
        if ( other.traits.Count != this.traits.Count )
        {
            Debug.LogError( "Incompatible personalities!" );
            moodHandler.SetNextMood( finalMood );
        }
        List<Trait>.Enumerator loop1 = traits.GetEnumerator();
        List<Trait>.Enumerator loop2 = other.traits.GetEnumerator();
        HashSet<PersonalityFactor>.Enumerator modelLoop = model.Factors.GetEnumerator();

        while ( modelLoop.MoveNext() & loop1.MoveNext() && loop2.MoveNext() )
        {
            Trait mine = loop1.Current;
            Trait his = loop2.Current;
            finalMood += modelLoop.Current.confront( mine, his );
        }
		if (finalMood.getFeel() == Mood.Feeling.SAD || finalMood.getFeel() == Mood.Feeling.PERPLEX)
        {
            // Debug.Log("Mood TieBreak for "+this);
            finalMood = Mood.HAPPY;
        }

        return finalMood;
    }
}

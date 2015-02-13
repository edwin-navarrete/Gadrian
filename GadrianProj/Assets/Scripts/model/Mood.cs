using UnityEngine;
using System;
using System.Collections;

/**
 * Represents a single emotional state of a character and can
 * interpolate towars another mood. 
 * */
public class Mood : IEquatable<Mood>{
	public readonly static Mood INDIFERENT = new Mood(Vector2.zero);
	public readonly static Mood SAD = new Mood( Vector3.up * 0.1f );
	public readonly static Mood HAPPY = new Mood ( Vector2.up );
	public readonly static Mood SCARED = new Mood ( Quaternion.AngleAxis ( -120, Vector2.up ) * Vector2.up );
	public readonly static Mood ANGRY = new Mood ( Quaternion.AngleAxis ( 120, Vector2.up ) * Vector2.up );

	public enum Feeling {
		INDIFERENT,
		SAD,
		HAPPY,
		ANGRY,
		SCARED,
		PERPLEX
	}

	//Mood is represented by a normalized 2d vector 
	private Vector2 value;

	#region Constructors

	public Mood(Vector2 value){
		if ( value.magnitude > 1.0f )
		{
			value.Normalize ();
		}
		this.value = value;
	}

	public Mood (Mood toCopyMood)
		:this (toCopyMood.value) { }

	public Mood ()
		: this ( Mood.INDIFERENT ) { }

	#endregion

	public bool Equals (Mood other)
	{
		if ( other == null ) return false;

		return value == other.value;
	}

	// Interpolates between mood 'a' and mood 'b' by t, with t between 0 and 1
	public static Mood Lerp(Mood a, Mood b, float t){
		Vector2 difference = ( b.value - a.value );
		if ( difference.magnitude < 0.1f )
		{
			return b;
		}

		difference *= t;	// t is a proportion of difference is going to be added to the current value
		Mood movedMood = new Mood ( new Vector2 ( a.value.x + difference.x, a.value.y + difference.y ) );
		return movedMood;
	}

	// One of the feelings corresponding with the current value of this mood
	public Feeling getFeel(){
		//FIXME implement method
		return Feeling.INDIFERENT;
	}

	//A number between 0 and one 
	public float getIntensity(){
		//NOTE returns the intensity of the current feeling.. for future use
		return value.magnitude;
	}

	public override string ToString ()
	{
		return string.Format ( "[Feel:{0}, Value:{1}]", getFeel(), value );
	}
}

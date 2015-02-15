using UnityEngine;
using System;
using System.Collections;

/**
 * Represents a single emotional state of a character and can
 * interpolate towars another mood. 
 * 
 * Tested by UnitTest MoodTest
 * */
public class Mood : IEquatable<Mood>{

	//The width of the perplex ring (equal to the 5 degree arc in units)
	private const float PERPLEX_THRESHOLD = 0.0872664625997165f;

	// The Basic Moods. Are Immutable by design.
	public readonly static Mood INDIFERENT = new Mood(Vector2.zero);
	public readonly static Mood SAD = new Mood( Vector3.up * 1e-6f );
	public readonly static Mood HAPPY = new Mood ( Quaternion.AngleAxis ( 60, Vector3.back ) * Vector2.up );
	public readonly static Mood SCARED = new Mood ( Quaternion.AngleAxis ( 300, Vector3.back ) * Vector2.up );
	public readonly static Mood ANGRY = new Mood ( Quaternion.AngleAxis ( 180, Vector3.back ) * Vector2.up );

	public enum Feeling {
		HAPPY,
		ANGRY,
		SCARED,
		INDIFERENT,
		SAD,
		PERPLEX
	}

	//Mood is represented by a normalized 2d vector 
	private Vector2 value;
	private bool accum = false;

	#region Constructors

	public Mood(Vector2 value){
		this.value = value;
	}

	public Mood(Vector2 value, bool accum){
		this.value = value;
		this.accum = accum;
	}

	public Mood (Mood toCopyMood)
		:this (toCopyMood.value) { }

	public Mood ()
		: this ( Mood.INDIFERENT ) { }

	#endregion

	// 
	// NOTE Sad + AnyMood = Sad, hence should never be used in a PersonalityFactor
	public static Mood operator +(Mood m1, Mood m2) 
	{
		if(m1.Equals(Mood.SAD) || m2.Equals(Mood.SAD)){
			Debug.Log("Backfall to SAD");
			return Mood.SAD;
		}
		return new Mood(m1.value + m2.value, true);
	}
	
	public bool Equals (Mood other)
	{
		if ( other == null ) return false;

		return value == other.value;
	}

	// Interpolates between mood 'a' and mood 'b' by t, with t between 0 and 1
	// This method cannot be invoked during a Mood accumulation because it will normalize it. 
	public static Mood Lerp(Mood a, Mood b, float t){
		return new Mood ( Vector3.Slerp(a.value, b.value, t) );
	}

	//Normalize the Mood to represent a single well defined Mood
	private void Normalize(){
		if ( value.magnitude > 1.0f || accum )
			value.Normalize();
	}

	// One of the feelings corresponding with the current value of this mood.
	// This method cannot be invoked during a Mood accumulation because it will normalize it. 
	public Feeling getFeel(){

		Normalize();

		float energyLevel = value.magnitude;
		if(energyLevel == 0f)
			return Feeling.INDIFERENT;

		if( Math.Abs(energyLevel - .5f) < PERPLEX_THRESHOLD)
			return Feeling.PERPLEX;

		if ( energyLevel < .5f )
			return Feeling.SAD;

		if(getIntensity() < PERPLEX_THRESHOLD)
			return Feeling.PERPLEX;

		float orientation = Vector2.Angle ( Vector2.up, value );
		if( value.x < 0)
			orientation = 360 - orientation;

		return (Feeling)Enum.ToObject(typeof(Feeling), (int)(orientation / 120));
	}

	//A number between 0 and one indicating how intense is the current feeling
	public float getIntensity(){
		if(value.magnitude < .5f)
			return 1f - value.magnitude * 2f;

		float orientation = Vector2.Angle ( Vector2.up, value );
		return 1f-Math.Abs(1f - (orientation % 120f)/60f);
	}

	public override string ToString ()
	{
		return string.Format ( "[Feel:{0}, Value:{1}]", getFeel(), getIntensity(), value );
	}
}

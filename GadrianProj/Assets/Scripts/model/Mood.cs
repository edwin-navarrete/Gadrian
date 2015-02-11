using UnityEngine;
using System;
using System.Collections;

/**
 * Represents a single emotional state of a character and can
 * interpolate towars another mood. 
 * */
public class Mood : IEquatable<Mood>{
	public const Mood INDIFERENT = new Mood(Vector2.zero);
	public const Mood SAD = new Mood( Vector2.up * 0.1 );
	public const Mood HAPPY = new Mood( Vector2.up );
	public const Mood SCARED = new Mood( Vector2.up * Quaternion.AngleAxis(-120, Vector2.up) );
	public const Mood ANGRY = new Mood( Vector2.up * Quaternion.AngleAxis(120, Vector2.up) );

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

	public Mood(Vector2 value){
		this.value = value.Normalize;
	}

	public override bool Equals( Object obj )
	{
		var other = obj as Mood;
		if( other == null ) return false;
		
		return value == other.value;             
	}


	// Interpolates between mood 'a' and mood 'b' by t, with t between 0 and 1
	public static Mood Slerp(Mood a, Mood b, float t){
		//FIXME implement method
		return INDIFERENT;
	}

	// One of the feelings corresponding with the current value of this mood
	public Feeling getFeel(){
		//FIXME implement method
		return INDIFERENT;
	}

	//A number between 0 and one 
	public float getIntensity(){
		//NOTE returns the intensity of the current feeling.. for future use
		return 1;
	}
}

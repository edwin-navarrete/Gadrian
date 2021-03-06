﻿using UnityEngine;
using System.Collections;

/**
 * Defines the current emotional state of the character and handles the transicion among the possible moods
 * by interpolating the next state and representing the mood with a face animation. 
 * */
public class MoodHandler : MonoBehaviour {

	private Mood current;
	private Mood next;
	private float speed = 0.1f;

	// Sets the first emotional state (indiferent by default)
	void Awake () {
		current = Mood.INDIFERENT;
		next = Mood.INDIFERENT;
	}
	
	// Updates and interpolates the emotional state 
	void Update () {
		if(current != next){
			current = Mood.Lerp(current, next, speed);
			//FIXME Update the character animation based on the Mood.. if neccessary
		}

	}

	// Set the new mood to start Lerping towards it
	public void SetNextMood (Mood nextMood)
	{
		next = nextMood;
	}

	public Mood NextMood
	{
		get
		{
			return next;
		}
	}

	public Mood CurrentMood
	{
		get
		{
			return current;
		}
	}
}

﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class PersonalityManager : MonoBehaviour
{
	// NOTE these booleans could be used for stablish which factors are going to be on this level
	public bool etnia = true;
	public bool complexion = true;
	public bool ears = true;

	// Use booleans to set enable = false on this components so they will be not take into account in personalityFactors
	private EtniaFactor etniaFactor;
	private ComplexionFactor complexionFactor;
	private EarAccessoryFactor earAccessoryFactor;

	private List<PersonalityFactor> personalityFactors;

	private static PersonalityModel personalityModel;
	public static PersonalityModel PersonalityModel
	{
		get
		{
			return personalityModel;
		}
	}

	private void Awake ()
	{
		etniaFactor = GetComponent<EtniaFactor> ();
		complexionFactor = GetComponent<ComplexionFactor> ();
		earAccessoryFactor = GetComponent<EarAccessoryFactor>();
	}

	private void Start ()
	{
		personalityFactors = new List<PersonalityFactor> ();
		AddComponents ();

		if ( personalityFactors == null )
			Debug.LogError ( "There is no components" );		

		CreatePersonalityModel ();
	}

	private void AddComponents ()
	{
		if ( etnia )
			personalityFactors.Add ( etniaFactor );

		if ( complexion )
			personalityFactors.Add ( complexionFactor );

		if ( ears )
			personalityFactors.Add ( earAccessoryFactor );

		if ( personalityFactors.Count == 0 )
			Debug.LogError("At least a factor is required!");
	}

	private void CreatePersonalityModel ()
	{
		HashSet<PersonalityFactor> factors = new HashSet<PersonalityFactor> ( personalityFactors );
		personalityModel = new PersonalityModel ( factors );
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("== List of Personalities ==");
        for (int i = 0; i < personalityModel.PersonalityCnt; i++)
        {
            string traitsStr = "";            
            foreach (Trait trait in personalityModel.getPersonalityTraits(i))
            {
                if (!System.String.IsNullOrEmpty(traitsStr))
                    traitsStr += ",";
                traitsStr += trait;
            }
            sb.AppendLine("["+i+"]="+ traitsStr);
        }
        Debug.Log(sb);
	}
}

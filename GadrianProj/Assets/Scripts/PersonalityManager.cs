using UnityEngine;
using System.Collections.Generic;

public class PersonalityManager : MonoBehaviour
{
	// NOTE these booleans could be used for stablish which factors are going to be on this level
	//public bool etnia = false;
	//public bool complexion = false;

	private PersonalityFactor[] personalityFactors;

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
		personalityFactors = GetComponents<PersonalityFactor> ();
		if ( personalityFactors == null )
			Debug.LogError ( "There is no components" );
	}

	private void Start ()
	{
		CreatePersonalityModel ();
	}

	private void CreatePersonalityModel ()
	{
		HashSet<PersonalityFactor> factors = new HashSet<PersonalityFactor> ( personalityFactors );
		personalityModel = new PersonalityModel ( factors );
	}
}

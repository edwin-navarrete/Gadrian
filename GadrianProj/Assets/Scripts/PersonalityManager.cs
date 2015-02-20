using UnityEngine;
using System.Collections.Generic;

public class PersonalityManager : MonoBehaviour
{
	// NOTE these booleans could be used for stablish which factors are going to be on this level
	public bool etnia = false;
	public bool complexion = false;

	// Use booleans to set enable = false on this components so they will be not take into account in personalityFactors
	private EtniaFactor etniaFactor;
	private ComplexionFactor complexionFactor;

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
		etniaFactor = GetComponent<EtniaFactor> ();
		complexionFactor = GetComponent<ComplexionFactor> ();

		DisableComponents ();
	}

	private void Start ()
	{
		personalityFactors = GetComponents<PersonalityFactor> ();
		if ( personalityFactors == null )
			Debug.LogError ( "There is no components" );

		CreatePersonalityModel ();
	}

	private void DisableComponents ()
	{
		etniaFactor.enabled = etnia;
		complexionFactor.enabled = complexion;
	}

	private void CreatePersonalityModel ()
	{
		HashSet<PersonalityFactor> factors = new HashSet<PersonalityFactor> ( personalityFactors );
		personalityModel = new PersonalityModel ( factors );
	}
}

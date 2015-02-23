using UnityEngine;
using System.Collections.Generic;

public class PersonalityManager : MonoBehaviour
{
	// NOTE these booleans could be used for stablish which factors are going to be on this level
	public bool etnia = true;
	public bool complexion = true;

	// Use booleans to set enable = false on this components so they will be not take into account in personalityFactors
	private EtniaFactor etniaFactor;
	private ComplexionFactor complexionFactor;

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
		if(personalityFactors.Count == 0)
			Debug.LogError("At least a factor is required!");
	}

	private void CreatePersonalityModel ()
	{
		HashSet<PersonalityFactor> factors = new HashSet<PersonalityFactor> ( personalityFactors );
		personalityModel = new PersonalityModel ( factors );
	}
}

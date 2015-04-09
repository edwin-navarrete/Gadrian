using UnityEngine;
using System.Collections.Generic;

public class PersonalityManager : Singleton<PersonalityManager>
{
    // NOTE these booleans could be used for stablish which factors are going to be on this level
    public bool etnia = true;
    public bool complexion = true;
    public bool headAccessory = true;

    // Use booleans to set enable = false on this components so they will be not take into account in personalityFactors
    private EtniaFactor etniaFactor;
    private ComplexionFactor complexionFactor;
    private HeadAccessoryFactor headAccessoryFactor;

    private List<PersonalityFactor> personalityFactors;

    private static PersonalityModel personalityModel;
    public static PersonalityModel PersonalityModel
    {
        get
        {
            return personalityModel;
        }
    }

    public void Awake ()
    {
        //FIXME make component recognition generic
        etniaFactor = GetComponent<EtniaFactor>();
        complexionFactor = GetComponent<ComplexionFactor>();
        headAccessoryFactor = GetComponent<HeadAccessoryFactor>();
    }

    public void InitializePersonalityModel ()
    {
        personalityFactors = new List<PersonalityFactor>();
        AddPersonalityComponents();

        if ( personalityFactors == null )
            Debug.LogError( "There is no components" );

        CreatePersonalityModel();
    }

    private void AddPersonalityComponents ()
    {
        if ( etnia )
            personalityFactors.Add( etniaFactor );
        if ( complexion )
            personalityFactors.Add( complexionFactor );
        if ( headAccessory )
            personalityFactors.Add( headAccessoryFactor );
        if ( personalityFactors.Count == 0 )
            Debug.LogError( "At least one factor is required!" );
    }

    private void CreatePersonalityModel ()
    {
        HashSet<PersonalityFactor> factors = new HashSet<PersonalityFactor>( personalityFactors );
        personalityModel = new PersonalityModel( factors );
    }
}

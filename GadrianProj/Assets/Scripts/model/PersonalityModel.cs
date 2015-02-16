using UnityEngine;
using System.Collections.Generic;

/**
 * Is a specific set of personality factors that will be used to represent
 * the personality of each character in the level
 * */
public class PersonalityModel  {

	HashSet<PersonalityFactor> factors;
	int personalityCnt = 0;

	public int PersonalityCnt
	{
		get
		{
			return personalityCnt;
		}
	}

	public HashSet<PersonalityFactor> Factors
	{
		get
		{
			return factors;
		}
	}

	public PersonalityModel(HashSet<PersonalityFactor> factors){
		this.factors = factors;
		this.personalityCnt = 0;
		foreach ( PersonalityFactor factor in factors )
		{
			this.personalityCnt += factor.getTraits().Count;	
		}
	}

	// return one of the personalities that can be represented with the set of factors it contains
	// if the i is bigger than personalityCnt, returns the i%personalityCnt-th personality
	public Personality getPersonality (int i) {

		if( i > personalityCnt)
			i = i % personalityCnt;

		int prevMult = 1;
		List<Trait> traitCombination = new List<Trait>();
		foreach ( PersonalityFactor factor in factors )
		{
			List<Trait> curFactorTraits = factor.getTraits();
			int curCnt = curFactorTraits.Count;
			traitCombination.Add(curFactorTraits[(i/prevMult) % curCnt]);
			prevMult *= curCnt;
		}
		//NOTE if this method is called many times with the same i, we should cache the personality
		return new Personality( traitCombination );
	}
	
}

using UnityEngine;
using System.Collections;

/**
 * Represents one of the aspects that conforms the personality of each character.
 * e.g. Musical Taste
 *
 * */
public interface IPersonalityFactor  {

	// Returns the mood resulting of putting together a character with a trait 'a' with another a trait 'b'
	// The traits must be of the same factor
	Mood confront(Trait a, Trait b);
}

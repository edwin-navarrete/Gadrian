using UnityEngine;
using System.Collections;

public class TintController : MonoBehaviour
{
	private Personality personality;
	private SnapCharacter snapCharacter;
	private SpriteRenderer backgroundRenderer;
	private Animator animator;

	public void Awake ()
	{
		personality = GetComponentInParent<Personality> ();
		snapCharacter = GetComponentInParent<SnapCharacter> ();
		backgroundRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();

		SubscribeEmotions ();
		snapCharacter.Intersecting += snapCharacter_Intersecting;
	}

	public void SubscribeEmotions ()
	{
		personality.Happy += () =>
		{
			animator.SetTrigger ( "newMood" );
			animator.SetInteger ( "mood", 1 );
		};

		personality.Sad += () =>
		{
			animator.SetTrigger ( "newMood" );
			animator.SetInteger ( "mood", 0 );
		};

		personality.Angry += () =>
		{
			animator.SetTrigger ( "newMood" );
			animator.SetInteger ( "mood", 2 );
		};

		personality.Scared += () =>
		{
			animator.SetTrigger ( "newMood" );
			animator.SetInteger ( "mood", 3 );
		};

		personality.Indifferent += () =>
		{
			animator.SetTrigger ( "newMood" );
			animator.SetInteger ( "mood", -1 );
		};
	}

	void snapCharacter_Intersecting (bool intersecting)
	{
		if ( intersecting )
		{
			backgroundRenderer.material.color = Color.red;
		}
		else
		{
			backgroundRenderer.material.color = Color.white;
		}
	}
}

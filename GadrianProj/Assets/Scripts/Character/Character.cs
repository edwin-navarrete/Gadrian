using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Character/Character")]
public class Character : MonoBehaviour
{
    public Personality personality;
    public SnapCharacter snapCharacter;
    public MoodHandler moodHandler;
    public BodyMatch bodyMatch;
}

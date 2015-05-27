using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Character/Face")]
public class Face : MonoBehaviour
{
    [SerializeField]
    private Transform faceSmallPosition;
    [SerializeField]
    private Transform faceFatPosition;
    [SerializeField]
    private Transform faceTallPosition;

    public void LocateFace (int complexion)
    {
        switch ( complexion )
        {
            case 0:
                this.transform.localPosition = faceSmallPosition.localPosition;
                break;

            case 1:
                this.transform.localPosition = faceFatPosition.localPosition;
                break;

            case 2:
                this.transform.localPosition = faceTallPosition.localPosition;
                break;

            default:
                Debug.LogError( "Recieved an invalid complexion value in Face script" );
                break;
        }
    }
}

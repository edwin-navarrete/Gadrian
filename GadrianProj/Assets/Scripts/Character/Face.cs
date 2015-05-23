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

    public void LocateFace (ComplexionTrait complexion)
    {
        Debug.LogFormat("The face to locate is {0}", complexion.ToString());

        if ( complexion == ComplexionFactor.SMALL )
        {
            this.transform.localPosition = faceSmallPosition.localPosition;
        }
        if ( complexion == ComplexionFactor.FAT )
        {
            this.transform.localPosition = faceFatPosition.localPosition;
        }
        if ( complexion == ComplexionFactor.TALL )
        {
            this.transform.localPosition = faceTallPosition.localPosition;
        }
    }
}

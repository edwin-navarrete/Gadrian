using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Gadrians.Util;

public class PetalGroup : MonoBehaviour
{
    [SerializeField]
    private List<Petal> leaves;

    public List<Petal> Leaves
    {
        get
        {
            return leaves;
        }
    }

    public bool AreAllLeavesClosed ()
    {
        foreach ( Petal leaf in leaves )
        {
            if ( leaf.State.Equals( LeafState.Opened ) || leaf.State.Equals( LeafState.Half ) )
            {
                return false;
            }
        }
        return true;
    }
}

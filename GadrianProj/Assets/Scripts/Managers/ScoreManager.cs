using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Gadrians.Util;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private int startingMoves;
    private int availableMoves;

    public int AvailableMoves
    {
        get
        {
            return availableMoves;
        }
    }
    
    public int Score { get; private set; }

    [SerializeField]
    private PetalGroup greenLeafs;
    [SerializeField]
    private PetalGroup yellowLeafs;
    [SerializeField]
    private PetalGroup redLeafs;

    private List<PetalGroup> allLeafGroups; 
    private PetalGroup currentGroup;

    [SerializeField]
    private Text scoreText;

    private void Awake ()
    {
        EventManager.StartListening( Events.MovedCharacter, ReduceMoves );

        allLeafGroups = new List<PetalGroup>();
        allLeafGroups.Add( greenLeafs );
        allLeafGroups.Add( yellowLeafs );
        allLeafGroups.Add( redLeafs );
    }

    private void Start ()
    {
        availableMoves = startingMoves;
        scoreText.text = availableMoves.ToString();
    }

    private void ReduceMoves ()
    {
        scoreText.text = (--availableMoves).ToString();
        currentGroup = getNextAvailableLeavesGroup();

        if ( availableMoves <= 0 || currentGroup == null )
        {
            EventManager.TriggerEvent( Events.Losing );
            EventManager.TriggerEvent( Events.Loss );
        }

        float fadeStep = 30f / startingMoves * availableMoves;
        float fadePercent = (Mathf.Round(fadeStep) % 2) / 2;
        FadeNextPetal( fadePercent );
    }

    private PetalGroup getNextAvailableLeavesGroup ()
    {
        foreach ( PetalGroup group in allLeafGroups )
        {
            if ( !group.AreAllLeavesClosed() )
            {
                return group;
            }
        }
        return null;
    }

    private void FadeNextPetal (float percent)
    {
        if ( currentGroup == null ) return;

        foreach ( Petal petal in currentGroup.Leaves )
        {
            if ( !petal.State.Equals(LeafState.Closed) && petal.FadePercent != percent )
            {
                petal.FadePetal( percent );
                return;
            }
        }
    }
}

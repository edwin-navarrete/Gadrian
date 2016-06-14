using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Gadrians.Util;

public class ScoreManager : Singleton<ScoreManager>
{
    private int configMoves = 15;
    private int remainingLeaves = 14;
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
        EventManager.StartListening(Events.MovedCharacter, ReduceMoves);
        EventManager.StartListening(Events.LevelGenerated, TruncateMoves);
        
        allLeafGroups = new List<PetalGroup>();
        allLeafGroups.Add( greenLeafs );
        allLeafGroups.Add( yellowLeafs );
        allLeafGroups.Add( redLeafs );
    }

    private void Start ()
    {
    }

    private void TruncateMoves(int moves)
    {
        startingMoves = moves;
        availableMoves = startingMoves;
        scoreText.text = availableMoves.ToString();

        int diff = configMoves - startingMoves;
        while ( diff-- > 0)
        {
            currentGroup = getNextAvailableLeavesGroup();
            FadeNextPetal(0f);
            configMoves--;
        }
        remainingLeaves = configMoves - 1;
    }

    private void ReduceMoves ()
    {
        if (availableMoves == 0)
            return;
        scoreText.text = (--availableMoves).ToString();
        currentGroup = getNextAvailableLeavesGroup();

        if ( availableMoves <= 0 || currentGroup == null )
        {
            EventManager.TriggerEvent( Events.Losing );
            EventManager.TriggerEvent( Events.Loss );
        }

        float leafStep = configMoves * availableMoves  / (float)startingMoves;
        float fadePercent = leafStep - (int)leafStep;
        if (remainingLeaves != (int)leafStep || fadePercent == 0) {
            FadeNextPetal(0);
        }
        if(fadePercent != 0)
        {
            FadeNextPetal(fadePercent);
        }
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
                if(percent == 0)
                    remainingLeaves--;
                petal.FadePetal( percent );
                return;
            }
        }
    }
}

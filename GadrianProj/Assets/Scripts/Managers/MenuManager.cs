using UnityEngine;
using System.Collections.Generic;

public class MenuManager : Singleton<MenuManager>
{
    private Animator animator;

    private int levelSelected;

    public int LevelSelected
    {
        get
        {
            return levelSelected;
        }
    }

    public void Awake ()
    {
        animator = GetComponent<Animator>();
    }

    #region Loading panel animation control

    public void LoadingScreen ()
    {
        animator.SetTrigger( "Loading" );
    }

    public void FadeInScreen ()
    {
        animator.SetTrigger( "FadeIn" );
    }

    public void FadeOutScreen ()
    {
        animator.SetTrigger( "FadeOut" );
    }

    #endregion
    
    public bool IsAnimationDone( string animationName, string layerName )
    {
        int layerIndex = animator.GetLayerIndex( layerName );
        AnimatorStateInfo clipState = animator.GetCurrentAnimatorStateInfo( layerIndex );
        if ( clipState.IsName( animationName ) )
        {
            return clipState.normalizedTime >= 1.0f ? true : false;
        }
        else
        {
            return false;
        }
    }
}
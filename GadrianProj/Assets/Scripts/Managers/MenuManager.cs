using UnityEngine;
using System.Collections.Generic;

public class MenuManager : Singleton<MenuManager>
{
    private int levelSelected;

    public int LevelSelected
    {
        get
        {
            return levelSelected;
        }
    }
}

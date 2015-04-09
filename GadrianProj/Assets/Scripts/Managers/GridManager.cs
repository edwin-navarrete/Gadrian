using UnityEngine;
using System.Collections.Generic;

public class GridManager : Singleton<GridManager>
{
    private GFGrid grid;

    public GFGrid Grid
    {
        get
        {
            if ( grid == null )
            {
                grid = GameObject.FindObjectOfType<GFGrid>();

                if ( grid == null )
                {
                    GameObject newGO = new GameObject( typeof( GFGrid ).ToString() );
                    grid = newGO.AddComponent<GFHexGrid>();
                }

                DontDestroyOnLoad( grid );
            }

            return grid;
        }
    }
}
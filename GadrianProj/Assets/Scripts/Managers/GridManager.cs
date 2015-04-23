using UnityEngine;
using System.Collections;

[AddComponentMenu("Grid Framework/GridManager")]
public class GridManager : Singleton<GridManager>
{
    private GFGrid grid;

    public GFGrid Grid
    {
        get
        {
            if ( grid == null )
            {
                grid = GetComponent<GFGrid>();

                if ( grid == null )
                {
                    grid = gameObject.AddComponent<GFHexGrid>();
                }
            }

            return grid;
        }
    }
}

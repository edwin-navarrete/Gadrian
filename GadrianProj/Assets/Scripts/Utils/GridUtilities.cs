using UnityEngine;
using System.Collections;

public static class GridUtilities
{
    public static Vector3 GridToWorldFixed (this GFGrid grid, Vector3 gridPosition)
    {
        Vector3 worldPosition = grid.GridToWorld( gridPosition );
        Vector3 fixedPosition = new Vector3( worldPosition.x * 0.75f, worldPosition.y * 1.15f, worldPosition.z );

        return fixedPosition;
    }

    public static Vector3 WorldToGridFixed (this GFGrid grid, Vector3 worldPosition)
    {
        Vector3 fixedPosition = new Vector3( worldPosition.x * 1.25f, worldPosition.y * 0.85f, worldPosition.z );
        Vector3 gridPosition = grid.WorldToGrid( fixedPosition );
        return gridPosition;
    }

    public static void AlignTransformFixed (this GFGrid grid, Transform transform)
    {
        grid.AlignTransform( transform );
        transform.position = new Vector3( transform.position.x * 0.75f, transform.position.y * 1.15f, transform.position.z );
    }
}

using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : UnityEngine.Component
{

    private static T instance;

    public static T Instance
    {
        get
        {
            if ( instance == null )
            {
                instance = GameObject.FindObjectOfType<T>();

                if ( instance == null )
                {
                    GameObject newGO = new GameObject( typeof( T ).ToString() );
                    instance = newGO.AddComponent<T>();
                }

                DontDestroyOnLoad( instance );
            }

            return instance;
        }
    }
}

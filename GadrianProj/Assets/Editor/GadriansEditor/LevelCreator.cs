using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class LevelCreator : EditorWindow
{
    [MenuItem("Gadrians/Level Generator")]
    private static void Init ()
    {
        // Get existing open window or if none, make a new one:
        LevelCreator window = (LevelCreator) EditorWindow.GetWindow( typeof( LevelCreator ) );
        window.Show();
    }

    public void OnGUI ()
    {
        GUILayout.Label( "Level Load", EditorStyles.boldLabel );
        GUIContent content = new GUIContent( "Create level", "Create a level from a text file and generate a level asset" );
        if ( GUILayout.Button(content, GUILayout.MinHeight(60.0f), GUILayout.MaxHeight(200.0f) ) )
        {
            CreateLevel();
        }
    }

    private void CreateLevel ()
    {
        // Get path of text file pregenerated level
        string loadPath = EditorUtility.OpenFilePanel( "Select level", "Resources/Levels/Pregenerated/", "level" );
        
        if ( loadPath != null )
        {
            // Create an instance of a new level to populate
            Level level = ScriptableObject.CreateInstance( typeof( Level ) ) as Level;
            // Read the file text where the level is
            StreamReader sr = new StreamReader( loadPath );
            string levelFile = sr.ReadToEnd();

            if ( levelFile != null )
            {
                string[] vectors = levelFile.Split( ',' );
                // Populate the level with each of the Vector2 found in the text file
                foreach ( string vector in vectors )
                {
                    string[] axis = vector.Split( ' ' );
                    Vector2 position = new Vector2( int.Parse( axis[0] ), int.Parse( axis[1] ) );
					int personalityIndex = int.Parse( axis[2] );
                    level.AddTilePosition( position, personalityIndex );
                }

                // Save the new generated level
                string uniquePath = AssetDatabase.GenerateUniqueAssetPath( "Assets/Resources/Levels/level.asset" );
                AssetDatabase.CreateAsset( level, uniquePath );
                AssetDatabase.SaveAssets();
            }
            else
            {
                Debug.LogError( "Text file is empty or can't recognize it's content" );
            }
        }
    }
}

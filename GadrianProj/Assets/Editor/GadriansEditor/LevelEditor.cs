using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    /// <summary>
    /// This is a Bitwise enum object that let the show method know what to render
    /// </summary>
    [Flags]
    public enum EditorListOption
    {
        None = 0,
        ListSize = 1,
        ListLabel = 2,
        ElementLabels = 4,
        Buttons = 8,
        Default = ListSize | ListLabel | ElementLabels,
        NoElementLabels = ListSize | ListLabel,
        All = Default | Buttons
    }

    private SerializedObject m_Level;
    private SerializedProperty m_TilesPosition;
    private SerializedProperty m_Moves;

    // Content used to create buttons on list elements
    private static GUIContent moveButtonContent = new GUIContent( "\u21b4", "move down" );
    private static GUIContent duplicateButtonContent = new GUIContent( "+", "duplicate" );
    private static GUIContent deleteButtonContent = new GUIContent( "-", "delete" );
    private static GUIContent addButtonContent = new GUIContent("+", "add element");
    private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

    public void OnEnable ()
    {
        m_Level = new SerializedObject( target );
        m_TilesPosition = m_Level.FindProperty( "tilesPosition" );
        m_Moves = m_Level.FindProperty( "availableMoves" );
    }

    public override void OnInspectorGUI ()
    {
        m_Level.Update();

        LevelEditor.Show( m_Moves, m_TilesPosition, EditorListOption.ListLabel | EditorListOption.Buttons );

        m_Level.ApplyModifiedProperties();
    }

    public static void Show (SerializedProperty moves, SerializedProperty list, EditorListOption options = EditorListOption.Default)
    {
        if (!list.isArray)
        {
			EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}

        bool showListLabel = ( options & EditorListOption.ListLabel ) != 0;
        bool showListSize = ( options & EditorListOption.ListSize ) != 0;
        
        if ( showListLabel )
        {
            EditorGUILayout.PropertyField( list );
            EditorGUI.indentLevel += 1;
        }
        if ( !showListLabel || list.isExpanded )
        {
            SerializedProperty size = list.FindPropertyRelative("Array.size");
			if (showListSize)
            {
				EditorGUILayout.PropertyField(size);
			}
			if (size.hasMultipleDifferentValues)
            {
				EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
			}
			else
            {
				ShowElements(moves, list, options);
			}
        }
        if ( showListLabel )
        {
            EditorGUI.indentLevel -= 1;
        }
    }

    private static void ShowElements (SerializedProperty moves, SerializedProperty list, EditorListOption options)
    {
        bool showElementLabels = ( options & EditorListOption.ElementLabels ) != 0;
        bool showButtons = (options & EditorListOption.Buttons) != 0;

        for ( int i = 0; i < list.arraySize; i++ )
        {
            if (showButtons)
            {
				EditorGUILayout.BeginHorizontal();
			}
            if ( showElementLabels )
            {
                EditorGUILayout.PropertyField( moves );
				SerializedProperty tileConfiguration = list.GetArrayElementAtIndex(i);
                //SerializedObject tileConfigurationObj = new SerializedObject(tileConfiguration.objectReferenceValue);
                ////Debug.LogFormat("tile congiguration is {0}", tileConfiguration);
                //SerializedProperty m_Position = tileConfigurationObj.FindProperty("position");
                //SerializedProperty m_PersonalityIndex = tileConfigurationObj.FindProperty("personalityIndex");
                //EditorGUILayout.PropertyField( m_Position );
                //EditorGUILayout.PropertyField( m_PersonalityIndex );
                EditorGUILayout.PropertyField(tileConfiguration);
				
            }
            else
            {
                EditorGUILayout.PropertyField( list.GetArrayElementAtIndex( i ), GUIContent.none );
            }
            if (showButtons)
            {
				ShowButtons( list, i );
                EditorGUILayout.EndHorizontal();
			}
            if (showButtons && list.arraySize == 0 && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
            {
			    list.arraySize += 1;
		    }
        }
    }

    private static void ShowButtons (SerializedProperty list, int index) {
		if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
			list.MoveArrayElement(index, index + 1);
		}
		if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
        {
			list.InsertArrayElementAtIndex(index);
		}
		if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
        {
			int oldSize = list.arraySize;
			list.DeleteArrayElementAtIndex(index);
			if (list.arraySize == oldSize)
            {
				list.DeleteArrayElementAtIndex(index);
			}
		}
	}
}
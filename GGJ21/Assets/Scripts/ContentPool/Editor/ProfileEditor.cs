using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContentManager))]
public class ProfileEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    ContentManager profiles = (ContentManager)target;

    //    GUILayout.Label("Profiles");
    //    profiles.profiles = EditorGUILayout.p
    //    profiles.SPANISH = EditorGUILayout.TextArea(langScript.SPANISH, textAreaOptions);
    //    GUILayout.Label("ENGLISH");
    //    langScript.ENGLISH = EditorGUILayout.TextArea(langScript.ENGLISH, textAreaOptions);
    //}

    SerializedProperty profiles;

    ContentManager cManager;

    private void OnEnable()
    {
        profiles = serializedObject.FindProperty("profiles");
        cManager = (ContentManager)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"), true);
        GUI.enabled = true;

        EditorGUILayout.PropertyField(profiles, true);
        serializedObject.ApplyModifiedProperties();
    }


}

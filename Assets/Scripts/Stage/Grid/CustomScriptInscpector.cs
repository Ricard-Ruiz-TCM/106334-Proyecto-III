using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Inspector2dArray))]
public class CustomScriptInscpector : Editor
{

    Inspector2dArray targetScript;

    void OnEnable()
    {
        targetScript = target as Inspector2dArray;
    }

    public override void OnInspectorGUI()
    {

        Inspector2dArray.X = EditorGUILayout.IntField(Inspector2dArray.X);
        Inspector2dArray.Y = EditorGUILayout.IntField(Inspector2dArray.Y);

        EditorGUILayout.BeginHorizontal();
        for (int y = 0; y < Inspector2dArray.Y; y++)
        {
            EditorGUILayout.BeginVertical();
            for (int x = 0; x < Inspector2dArray.X; x++)
            {

                targetScript.columns[x].rows[y] = EditorGUILayout.Toggle(targetScript.columns[x].rows[y]);
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();

    }
}
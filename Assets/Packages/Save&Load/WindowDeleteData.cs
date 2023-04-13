using System.IO;
using UnityEditor;
using UnityEngine;


public class WindowDeleteData : EditorWindow {
    private static string fileName;

    [MenuItem("Custom/DeleteSaveFile")]
    public static void DeleteFile() {
        WindowDeleteData window = (WindowDeleteData)EditorWindow.GetWindow(typeof(WindowDeleteData), true, "DataReset");

    }


    void OnGUI() {
        GUILayout.Label("File Name", EditorStyles.boldLabel);
        fileName = EditorGUILayout.TextField("Name", fileName);

        if (GUILayout.Button("Delete")) {
            File.Delete(Path.Combine(Application.persistentDataPath, fileName));
        }
    }
}


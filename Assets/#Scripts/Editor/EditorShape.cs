using UnityEngine;
using UnityEditor;


//[CustomEditor(typeof(Shape))]
public class EditorShape : Editor {

    // Shape targetScript;
    //
    // void OnEnable(){
    //     targetScript = target as Shape;
    // }
    //
    // public override void OnInspectorGUI()
    // {
    //     base.OnInspectorGUI();
    //     
    //     EditorGUILayout.BeginHorizontal ();
    //     for (int x = 0; x < 3; x++) {
    //         EditorGUILayout.BeginVertical (GUILayout.MaxWidth(10));
    //         for (int y = 0; y < 3; y++)
    //         {
    //             targetScript.pieces[x,y] = EditorGUILayout.Toggle (targetScript.pieces[x,y]);
    //         }
    //         EditorGUILayout.EndVertical ();
    //
    //     }
    //     EditorGUILayout.EndHorizontal ();
    // }
}
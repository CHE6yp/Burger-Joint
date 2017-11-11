using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Usable))]
public class UsableCI : Editor {

    public override void OnInspectorGUI()
    {
        try
        {
            Usable script = (Usable)target;
            //new string[script.useArray.Length];
            
            foreach (KeyValuePair<string,Usable.UsableFunc> func in script.useDict)
            {
                EditorGUILayout.LabelField(func.Key, func.Value.Method.Name);
            }
        }
        catch { }
    }
}
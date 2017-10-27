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

[CustomEditor(typeof(AvailableUses))]
public class AvailableUsesCI : Editor
{

    public override void OnInspectorGUI()
    {
        try
        {
            AvailableUses script = (AvailableUses)target;
            //new string[script.useArray.Length];
            ///EditorGUILayout.LabelField("Yo");

            foreach (KeyValuePair<Usable, Dictionary<string, Usable.UsableFunc>> dict in script.currentUses)
            {
                EditorGUILayout.LabelField(dict.Key.gameObject.name, EditorStyles.boldLabel);
                foreach (KeyValuePair<string,  Usable.UsableFunc> uses in dict.Value)
                {
                    EditorGUILayout.LabelField(uses.Key);
                }
                //EditorGUILayout.LabelField(di.Key, func.Value.Method.Name);
                EditorGUILayout.Space();
            }
        }
        catch { }
    }

    [CustomEditor(typeof(GroundItem))]
    public class GroundItemCI : Editor
    {

        public override void OnInspectorGUI()
        {
            try
            {
                GroundItem script = (GroundItem)target;
                //new string[script.useArray.Length];

                foreach (Player player in script.players)
                {
                    EditorGUILayout.LabelField(player.gameObject.name);
                }
            }
            catch { }
        }
    }
}
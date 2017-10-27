using UnityEngine;
using System.Collections.Generic;


public class Usable : MonoBehaviour {

    public delegate void UsableFunc(Player player);
    public UsableFunc use;
    
    public Dictionary<string, UsableFunc> useDict = new Dictionary<string, UsableFunc>();

    //кажись тоже не нужно уже
    public string ShowUsable()
    {
        string usableList = "";
        foreach (KeyValuePair<string,UsableFunc> func in useDict)
        {
            string nT = func.Key;
            usableList += (func.Value.Method.Name+ ": " + nT + " \n");
        }
            
        
        return usableList;
    }

    public void Switch(string delete, string addKey,UsableFunc addValue)
    {
        try
        {
            useDict.Remove(delete);
        }
        catch { }
        try
        {
            //if (useDict[addKey] != addValue)
                useDict.Add(addKey, addValue);
        }
        catch { }

        UIManager.uiManager.ContextRedraw(PlayerController.player);
    }
}

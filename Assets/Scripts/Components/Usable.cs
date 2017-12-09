using UnityEngine;
using System.Collections.Generic;


public class Usable : MonoBehaviour {

    public delegate void UsableFunc(Player player);
    
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
        catch
        {
            Debug.LogWarning("Usable.Switch() some error in useDict.Remove");
        }
        try
        {
            useDict.Add(addKey, addValue);
        }
        catch
        {
            Debug.LogWarning("Usable.Switch() some error in useDict.Add");
        }

        //UIManager.uiManager.ContextRedraw(PlayerController.player);
    }
}

using UnityEngine;
using System.Collections;

public class Meat : MonoBehaviour {
    Usable usable;

    public bool breaded = false;
    public enum CookState { Raw, UnderCooked, WellCooked, OverCooked, Burnt}
    public CookState cookState = CookState.Raw;
    public float cookPoint = 0;
    public MeshRenderer looks;

    public Material[] mats = new Material[5];


	// Use this for initialization
	void Start () {
        usable = GetComponent<Usable>();
        usable.useDict.Add("Bread", Bread);
	}
	
	public void Bread(Player player)
    {
        if (GetComponent<Placable>().itemPlaceOfParent.GetComponent<BreadTable>() != null)
        {
            breaded = true;
        }
    }

    public void Cook(float point)
    {
        cookPoint += point;
        if (cookPoint < 800f)
            cookState = CookState.Raw;
        if (cookPoint >= 800f && cookPoint < 1400f)
        {
            cookState = CookState.UnderCooked;
            looks.material = mats[1];
        }
        if (cookPoint >= 1400f && cookPoint < 1800f)
        {
            cookState = CookState.WellCooked;
            looks.material = mats[2];
        }
        if (cookPoint >= 1800f && cookPoint < 2400f)
        {
            cookState = CookState.OverCooked;
            looks.material = mats[3];
        }
        if (cookPoint >= 2400f)
        {
            cookState = CookState.Burnt;
            looks.material = mats[4];
        }
    }
}

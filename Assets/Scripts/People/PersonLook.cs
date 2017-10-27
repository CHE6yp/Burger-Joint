using UnityEngine;
using System.Collections;

public class PersonLook : MonoBehaviour {

    public Renderer head;
    public Renderer torso;
    public Renderer handLU;
    public Renderer handLD;
    public Renderer handRU;
    public Renderer handRD;
    public Renderer legLU;
    public Renderer legLD;
    public Renderer legRU;
    public Renderer legRD;
    [Space(10)]
    public GameObject hatPlace;
    public GameObject cap;
    public GameObject glassesPlace;
    public GameObject glasses;
    public Renderer capB;
    public Renderer capT;
    [Space(10)]
    public Material skin_black;
    public Material skin_white;
    [Space(5)]
    public Material shirt_green;
    public Material shirt_blue;
    public Material shirt_yellow;
    public Material shirt_red;
    public Material shirt_black;
    public Material shirt_white;

    // Use this for initialization
    void Start () {

        RandomLook();


    }

    void RandomLook()
    {
        //кожа
        if (Random.Range(0, 3) != 0)
            head.material = skin_white;
        else
            head.material = skin_black;
        #region top
        //футболка
        int i = Random.Range(0, 6);
        switch (i)
        {
            case 0:
                torso.material = shirt_black;
                handLU.material = shirt_black;
                handRU.material = shirt_black;
                break;
            case 1:
                torso.material = shirt_white;
                handLU.material = shirt_white;
                handRU.material = shirt_white;
                break;
            case 2:
                torso.material = shirt_green;
                handLU.material = shirt_green;
                handRU.material = shirt_green;
                break;
            case 3:
                torso.material = shirt_yellow;
                handLU.material = shirt_yellow;
                handRU.material = shirt_yellow;
                break;
            case 4:
                torso.material = shirt_blue;
                handLU.material = shirt_blue;
                handRU.material = shirt_blue;
                break;
            case 5:
                torso.material = shirt_red;
                handLU.material = shirt_red;
                handRU.material = shirt_red;
                break;
        }

        //рукава
        if (Random.Range(0, 3) != 0)
        {
            handLD.material = head.material;
            handRD.material = head.material;
        }
        else
        {
            handLD.material = torso.material;
            handRD.material = torso.material;
        }
        #endregion

        #region bottom

        i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                legLU.material = shirt_black;
                legRU.material = shirt_black;
                break;
            case 1:
                legLU.material = shirt_blue;
                legRU.material = shirt_blue;
                break;
            case 2:
                legLU.material = shirt_white;
                legRU.material = shirt_white;
                break;
        }

        //шорты или штаны?
        if (Random.Range(0, 5) == 0)
        {
            legLD.material = head.material;
            legRD.material = head.material;
        }
        else
        {
            legLD.material = legLU.material;
            legRD.material = legRU.material;
        }
        #endregion


        //есть ли кепка
        if (Random.Range(0, 3) == 0)
        {
            GameObject capTemp = GameObject.Instantiate(cap);
            capTemp.name = "CapBottom";
            capTemp.transform.parent = hatPlace.transform;
            capTemp.transform.localPosition = new Vector3(0, 0, 0);
            capTemp.transform.localRotation = new Quaternion(0, 0, 0, 0);

            //цвет кепки
            int i2 = Random.Range(0, 6);
            switch (i2)
            {
                case 0:
                    capB.material = shirt_black;
                    capT.material = shirt_black;
                    break;
                case 1:
                    capB.material = shirt_white;
                    capT.material = shirt_white;
                    break;
                case 2:
                    capB.material = shirt_green;
                    capT.material = shirt_green;
                    break;
                case 3:
                    capB.material = shirt_yellow;
                    capT.material = shirt_yellow;
                    break;
                case 4:
                    capB.material = shirt_blue;
                    capT.material = shirt_blue;
                    break;
                case 5:
                    capB.material = shirt_red;
                    capT.material = shirt_red;
                    break;
            }


        }

        //очки
        if (Random.Range(0, 5) == 0)
        {
            GameObject glassesTemp = GameObject.Instantiate(glasses);
            glassesTemp.name = "Glasses";
            glassesTemp.transform.parent = glassesPlace.transform;
            glassesTemp.transform.localPosition = new Vector3(0, 0, 0);
            glassesTemp.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }



    }


}

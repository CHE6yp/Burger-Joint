using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    bool menuShown;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
                MenuToggle();
	}

    void MenuToggle()
    {
        if (menuShown)
        {
            menuShown = false;
            mainMenu.SetActive(false);
        }
        else
        {
            menuShown = true;
            mainMenu.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("1");
    }
}

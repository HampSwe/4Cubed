using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {


    public GameObject uiPanelTransistion;
    public GameObject introPanel;


    public int sceneToLoad;
    public bool exit = false;


	// Use this for initialization
	void Start ()
    {
        exit = false;

        uiPanelTransistion.SetActive(false);

        introPanel.SetActive(true);


        if (!PlayerPrefs.HasKey("ScoreSum")) //if it's the first time started, it resets the statistics
        {
            PlayerPrefs.SetFloat("ScoreSum", 0);
        }

        if (!PlayerPrefs.HasKey("GamesPlayed"))
        {
            PlayerPrefs.SetInt("GamesPlayed", 0);
        }

        if (!PlayerPrefs.HasKey("pUCollected"))
        {
            PlayerPrefs.SetInt("pUCollected", 0);
        }

        PlayerPrefs.Save();

    }




    public void LoadNewScene()
    {
        if (!exit)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Application.Quit();
        }


    }


    public void LoadStartMenu()
    {
        sceneToLoad = 0;

        uiPanelTransistion.SetActive(true);
    }


    public void LoadGameScene()
    {

        sceneToLoad = 1;

        uiPanelTransistion.SetActive(true);
    }


    public void LoadCreditsMenu()
    {

        sceneToLoad = 2;

        uiPanelTransistion.SetActive(true);
    }


    public void LoadStatsMenu()
    {

        sceneToLoad = 3;

        uiPanelTransistion.SetActive(true);
    }


    public void LoadHelpMenu()
    {

        sceneToLoad = 4;

        uiPanelTransistion.SetActive(true);
    }


    public void ExitGame()
    {
        exit = true;

        uiPanelTransistion.SetActive(true);
    }

}

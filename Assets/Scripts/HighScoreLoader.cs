using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class HighScoreLoader : MonoBehaviour
{

    /*
    private string fileName = "high_score.txt";
    private string subPath = @"\SaveData";
    private string path;
    private string filePath;
    */



    public string LoadHighScore() //loads the highscore
    {

        string tmp;

        try
        {
            tmp = PlayerPrefs.GetString("highscore");

        }
        catch (Exception e)
        {
            tmp = "error"; //if an error occures reading the file

            Debug.Log("ERROR LOADING HIGHSCORE: " + e);
        }

        return tmp;
    }


    public void SetHighScore(string highscore)
    {

        PlayerPrefs.SetString("highscore", highscore);
        PlayerPrefs.Save();

        
    }



}

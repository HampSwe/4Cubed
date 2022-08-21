using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class HighScoreScript : MonoBehaviour
{

    public Text myText;


    /*
    private string fileName = "high_score.txt";
    private string subPath = @"\SaveData";
    private string path;
    private string filePath;
    */

    public string currentContent;




    private void Start()
    {

        //File.Decrypt(path);
        
        currentContent = LoadHighScoreNew();

        if (currentContent == "error")
        {
            myText.text = "";
        }
        else
        {
            myText.text = "high-score <color=" + PlayerPrefs.GetString("MyColor") + ">" + currentContent + "</color>"; //displays the score, in the correct color
        }

              

    }

    /*
    public void SetHighScore(string highscore) //Sets the highscore
    {

        path = Application.dataPath + subPath; //finds the path to the data file on the computer
        filePath = Path.Combine(path, fileName);


        File.WriteAllText(filePath, highscore); //writes to the data-file the new highscore

        //File.Encrypt(filePath);

        currentContent = highscore;

        myText.text = "high-score <color=#" + color + ">" + highscore + "</color>"; //Displays the new highscore
    }


    public string LoadHighScore() //loads the highscore
    {

        path = Application.dataPath + subPath; //finds the path to the data file on the computer
        filePath = Path.Combine(path, fileName);


        string tmp;

        try
        {
            tmp = File.ReadAllText(filePath);  //reads the file

        }
        catch (Exception e)
        {
            tmp = "error"; //if an error occures reading the file

            Debug.Log("ERROR LOADING HIGHSCORE: " + e);
        }

        return tmp;
    }

    */

    public void SetHighScoreNew(string highscore)
    {

        PlayerPrefs.SetString("highscore", highscore);
        PlayerPrefs.Save();

        currentContent = highscore;

        myText.text = "high-score <color=" + PlayerPrefs.GetString("MyColor") + ">" + highscore + "</color>"; //Displays the new highscore
    }


    public string LoadHighScoreNew()
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


    //VAD HÄNDER FÖRSTA GÅNGEN, OM DET INTE FINNS NÅGOT HIGHSCORE?





}
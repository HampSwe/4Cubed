using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{


    public Text scoreText;
    float timer;
    float roundTimer;
    public bool startTimer = true;

    string scoreAchieved;
    public string highScore;
    bool ignoreHS;
    bool random;

    public GameObject hScriptObj;
    public GameObject showScript;

    string[,] colors = new string[6, 2];
    float currentHighScore;
    List<int> unlockedColors = new List<int>();

    // Use this for initialization
    void Start()
    {
        startTimer = true;

        colors[0, 0] = "#8AB4E8";
        colors[1, 0] = "#FB9494";
        colors[2, 0] = "#FFBF69";
        colors[3, 0] = "#9ABA82";
        colors[4, 0] = "#BDA0BC";
        colors[5, 0] = "#FF966D";

        colors[0, 1] = "0";
        colors[1, 1] = "10";
        colors[2, 1] = "25";
        colors[3, 1] = "40";
        colors[4, 1] = "60";
        colors[5, 1] = "80";

        currentHighScore = float.Parse(PlayerPrefs.GetString("highscore"));

        if (PlayerPrefs.GetInt("LastIndex") == PlayerPrefs.GetInt("ColorLength"))
        {
            random = true;

            for (int i = 0; i < colors.GetLength(0); i++)
            {
                if (currentHighScore >= float.Parse(colors[i, 1]))
                {
                    unlockedColors.Add(i);
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            random = false;
        }


        Color col;
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("MyColor"), out col); //changes the color
        scoreText.color = col;


        HighScoreScript h = hScriptObj.GetComponent<HighScoreScript>();

        highScore = h.LoadHighScoreNew();

        if (highScore == "error")
        {
            ignoreHS = true;
        }
        else
        {
            ignoreHS = false;
        }

        timer = 0;
        scoreText.text = "0.0";

    }

    public ColorSetScript cSetScript;

    public void updateMyColor()
    {
        if (random) //if the "random-color" is enabled, pick a new random color
        {
            PlayerPrefs.SetString("MyColor", colors[Random.Range(0, unlockedColors.Count), 0]);  //randomly picks one of the colors

            cSetScript.updateMyColor();

            Color col;
            ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("MyColor"), out col); //changes the color
            scoreText.color = col;
        }
    }



    // Update is called once per frame
    void Update()
    {

        //counts the timer

        if (startTimer) //Checks that the timer is started
        {
            timer += Time.deltaTime;

            roundTimer = Mathf.Round(timer * 10) / 10; //rounds the timer to tenths


            if (roundTimer - Mathf.Round(roundTimer) == 0) //code that ensures the time ends with an .0
            {
                scoreText.text = roundTimer.ToString() + ".0";
            }
            else
            {
                scoreText.text = roundTimer.ToString();
            }
        }
        else
        {
            timer = 0;
        }
    }


    float lastColorScore;
    public float respawnTime;


    public void ResetScore() //resets the score, and checks for highscore
    {
        if (roundTimer - Mathf.Round(roundTimer) == 0) //security so that it wont use the screen number, this could easily be changed
        {
            scoreAchieved = roundTimer.ToString() + ".0"; //adds the .0 if the number didn't have a decimal, since the float type otherwise ignores this

        }
        else
        {
            scoreAchieved = roundTimer.ToString();
        }


        PlayerPrefs.SetFloat("ScoreSum", PlayerPrefs.GetFloat("ScoreSum") + roundTimer); //adds values to the statistics
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);

        if (PlayerPrefs.HasKey("C" + PlayerPrefs.GetInt("LastIndex"))) //checks if the player has set a score before with this color
        {
            lastColorScore = PlayerPrefs.GetFloat("C" + PlayerPrefs.GetInt("LastIndex")); //gets the last score from the current color
        }
        else
        {
            lastColorScore = 0;
        }

        PlayerPrefs.SetFloat("C" + PlayerPrefs.GetInt("LastIndex"), lastColorScore + roundTimer + respawnTime); //adds the score to the current color
        PlayerPrefs.Save();


        string displayMessage = scoreAchieved;

        if (!ignoreHS)
        {
            if (float.Parse(scoreAchieved) > float.Parse(highScore)) //checks if the current score achieved is more than the highscore
            {
                highScore = scoreAchieved;

                HighScoreScript h = hScriptObj.GetComponent<HighScoreScript>();

                h.SetHighScoreNew(highScore); //Sets a new highscore

                displayMessage = "New Highscore\n" + scoreAchieved;
            }
        }


        ShowYourScore show = showScript.GetComponent<ShowYourScore>(); //shows the last score in the middle of the screen
        showScript.SetActive(true);
        show.ShowScore(displayMessage);


        timer = 0; //resets the timer
        //scoreText.text = "0.0";
    }


}

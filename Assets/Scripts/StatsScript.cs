using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{

    public Text myText;
    public GameObject colorDisplay;
    public GameObject die;

    string averageScore;
    string highScore;
    string gamesPlayed;
    string pUcollected;
    string text;
    string favColorString;


    // Use this for initialization
    void Start()
    {
        float[] colorScores = new float[PlayerPrefs.GetInt("ColorLength") + 1]; //makes sure that the amount of colors in the array corresponds to the real amount of colors
        int[] colorOrder = new int[PlayerPrefs.GetInt("ColorLength") + 1];


        if (!(PlayerPrefs.GetInt("GamesPlayed") == 0))
        {
            averageScore = (Mathf.Round((((PlayerPrefs.GetFloat("ScoreSum")) / ((float)PlayerPrefs.GetInt("GamesPlayed")))) * 10) / 10).ToString();
        }
        else
        {
            averageScore = "n/a";
        }

        highScore = PlayerPrefs.GetString("highscore");

        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed").ToString();

        pUcollected = PlayerPrefs.GetInt("pUCollected").ToString();


        for (int i = 0; i < colorScores.Length; i++) //sets the color lists
        {
            colorScores[i] = PlayerPrefs.GetFloat("C" + i);
            colorOrder[i] = i;
        }

        HubbleSort(colorScores, ref colorOrder); //sorts the lists


        if (colorOrder[colorOrder.Length - 1] == PlayerPrefs.GetInt("ColorLength")) //checks if the most used is the die or a color
        {
            //shows the die;
            die.SetActive(true);
            colorDisplay.SetActive(false);
        }
        else
        {
            die.SetActive(false);
            colorDisplay.SetActive(true);

            favColorString = PlayerPrefs.GetString("CVALUE" + colorOrder[colorOrder.Length - 1]); //gets the most used color´s hex-value
            Image image = colorDisplay.GetComponent<Image>();
            Color col;
            ColorUtility.TryParseHtmlString(favColorString, out col); //changes the color of the displayImage
            image.color = col;
        }



        //displays the text
        text = "Average score " + "<color=#F9DEC9FF>" + averageScore + "</color>\n" +
            "Highscore " + "<color=#F9DEC9FF>" + highScore + "</color>\n" +
            "Games Played " + "<color=#F9DEC9FF>" + gamesPlayed + "</color>\n" +
            "Power-Ups " + "<color=#F9DEC9FF>" + pUcollected + "</color>\n" +
            "Favorite color ";

        myText.text = text;


    }

    float tmpValue;
    int tmpValue2;

    public void HubbleSort(float[] scores, ref int[] colors) //a method for sorting the colors in which is the most used
    {
        for (int j = scores.Length - 1; j >= 0; j--)
        {
            for (int i = 0; i < j; i++)
            {
                if (scores[i] > scores[i + 1])
                {
                    tmpValue = scores[i + 1];
                    scores[i + 1] = scores[i];
                    scores[i] = tmpValue;

                    tmpValue2 = colors[i + 1];
                    colors[i + 1] = colors[i];
                    colors[i] = tmpValue2;
                }
            }
        }
    }
}

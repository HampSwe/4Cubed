using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManagerScript : MonoBehaviour
{


    public GameObject colorDisplay;
    public GameObject scoreLoader;
    public GameObject lockedSymbol;
    public GameObject reqScore;
    public GameObject die;
    public Text reqScoreText;

    public Text highScoreText;
    public GameObject highScoreObject;

    public GameObject errorMessage;


    int colorIndex;
    float currentHighScore;
    float requiredScore;

    string[,] colors = new string[6, 3];
    string loadedStringScore;


    // Use this for initialization
    void Start()
    {

        PlayerPrefs.SetString("MyColor", colors[0, 1]);
        PlayerPrefs.SetInt("ColorLength", colors.GetLength(0));

        if (!(PlayerPrefs.HasKey("played"))) //checks if the game is played or not
        {
            PlayerPrefs.SetString("played", "yes");
            PlayerPrefs.SetString("highscore", "0.0");
            PlayerPrefs.SetInt("LastIndex", 0);
            highScoreObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("highscore") == "0.0")
        {
            highScoreText.text = "highscore " + PlayerPrefs.GetString("highscore");
            highScoreObject.SetActive(false);
            currentHighScore = 0;
        }
        else
        {
            highScoreObject.SetActive(true);
            highScoreText.text = "highscore " + PlayerPrefs.GetString("highscore"); //displays the highscore
            currentHighScore = float.Parse(PlayerPrefs.GetString("highscore"));
        }


        //SETS THE COLOR-DATA
        //IF YOU CHANGE THESE, YOU ALSO HAVE TO UPDATE THEM TO SCORESCRIPT, DUE TO THE RANDOM-PICKER

        colors[0, 0] = "blue";
        colors[1, 0] = "red";
        colors[2, 0] = "yellow";
        colors[3, 0] = "olive";
        colors[4, 0] = "purple";
        colors[5, 0] = "orange";


        colors[0, 1] = "#8AB4E8";
        colors[1, 1] = "#FB9494";
        colors[2, 1] = "#FFBF69";
        colors[3, 1] = "#9ABA82";
        colors[4, 1] = "#BDA0BC";
        colors[5, 1] = "#FF966D";

        for (int i = 0; i < colors.GetLength(0); i++)
        {
            PlayerPrefs.SetString("CVALUE" + i, colors[i, 1]);
        }

        colors[0, 2] = "0";
        colors[1, 2] = "10";
        colors[2, 2] = "25";
        colors[3, 2] = "40";
        colors[4, 2] = "60";
        colors[5, 2] = "80";


        try
        {
            colorIndex = PlayerPrefs.GetInt("LastIndex"); //finds the last chosen color, and updates it
        }
        catch
        {
            colorIndex = 0;
        }


        UpdateColor();


    }



    public void ColorRight() //when the right-arrow button is pressed, switch color
    {

        if (colorIndex < PlayerPrefs.GetInt("ColorLength")) //set to 6 because there are currently five colors in the game
        {
            colorIndex++;
        }
        else
        {
            colorIndex = 0;
        }

        UpdateColor();
    }


    public void ColorLeft() //when the left-arrow button is pressed, switch color
    {
        if (colorIndex > 0)
        {
            colorIndex--;
        }
        else
        {
            colorIndex = PlayerPrefs.GetInt("ColorLength");
        }

        UpdateColor();
    }



    public void UpdateColor()
    {

        if (colorIndex == PlayerPrefs.GetInt("ColorLength")) //checks if the die is selected
        {
            die.SetActive(true); //shows the die
            reqScore.SetActive(false);
            colorDisplay.SetActive(false);
            lockedSymbol.SetActive(false);

            List<int> unlockedColors = new List<int>(); //finds all the unlocked colors
            unlockedColors = UnlockedColors();

            int colorItem = Random.Range(0, unlockedColors.Count); //randomly picks one of the colors

            PlayerPrefs.SetString("MyColor", colors[colorItem, 1]);
            PlayerPrefs.SetInt("LastIndex", colorIndex);

            //PlayerPrefs.SetString("MyColor", colors[Random.Range(0, PlayerPrefs.GetInt("ColorLength") - 1), 1]);

        }
        else
        {

            requiredScore = float.Parse(colors[colorIndex, 2]);
            die.SetActive(false);

            if (currentHighScore >= requiredScore) //checks if the color is unlocked
            {

                colorDisplay.SetActive(true); //shows the color, hides the locked-symbol and the die
                lockedSymbol.SetActive(false);
                reqScore.SetActive(false);

                Image image = colorDisplay.GetComponent<Image>();
                Color col;

                ColorUtility.TryParseHtmlString(colors[colorIndex, 1], out col); //changes the color of the displayImage

                image.color = col;

                PlayerPrefs.SetString("MyColor", colors[colorIndex, 1]);
                PlayerPrefs.SetInt("LastIndex", colorIndex);

            }
            else
            {

                colorDisplay.SetActive(false); //hides the color, shows the locked-symbol
                lockedSymbol.SetActive(true);

                PlayerPrefs.SetString("MyColor", colors[0, 1]);
                PlayerPrefs.SetInt("LastIndex", 0);

                reqScoreText.text = "score " + colors[colorIndex, 2] + " to unlock"; //displays the required score to unlock the color
                reqScore.SetActive(true);


            }

        }

        PlayerPrefs.Save();


    }


    public List<int> UnlockedColors() //a method to get the unlocked colors
    {
        List<int> tmpList = new List<int>();

        for (int i = 0; i < colors.GetLength(0); i++)
        {
            if (currentHighScore >= float.Parse(colors[i, 2]))
            {
                tmpList.Add(i);
            }
            else
            {
                break;
            }
        }

        return tmpList;
    }


}

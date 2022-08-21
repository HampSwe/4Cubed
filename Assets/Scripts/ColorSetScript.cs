using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetScript : MonoBehaviour
{


    public Material mat; //blink color
    public Material mat2;

    string redColor1 = "#FB9494";
    public string redColor2;


    // Use this for initialization
    void Start()
    {
           
        Color col1;

        if (PlayerPrefs.GetInt("LastIndex") == 1) //if the player uses the red color, make the blink-color another hue of red
        {
            ColorUtility.TryParseHtmlString(redColor2, out col1); //changes the color
        }
        else
        {
            ColorUtility.TryParseHtmlString(redColor1, out col1); //changes the color

        }
        mat.color = col1;


        Color col2;
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("MyColor"), out col2); //changes the color
        mat2.color = col2;


    }


    public void updateMyColor()
    {
        Color col2;
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("MyColor"), out col2); //changes the color
        mat2.color = col2;
    }


}

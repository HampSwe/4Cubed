using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperScript : MonoBehaviour
{

    public GameObject inputFieldObj;
    public InputField inputText;
    public Text placeHolderText;

    bool developer = false;
    string code = "ftswo";


    private void Start()
    {
        inputFieldObj.SetActive(false);


        try
        {
            if (PlayerPrefs.GetString("developer") == "true")
            {
                developer = true;
            }
            else
            {
                developer = false;
            }
        }
        catch
        {
            developer = false;
        }

    }


    public void ClickOnButton()
    {
        inputFieldObj.SetActive(true);

        if (developer)
        {
            placeHolderText.text = "Set highscore";
        }
        else
        {
            placeHolderText.text = "Enter code";
        }
    }


    public void InputMethod()
    {

        if (developer)
        {
            try
            {
                string tmp = inputText.text;
                float tmp2 = float.Parse(tmp); //just to make sure it's a float
                tmp2 = tmp2++;

                PlayerPrefs.SetString("highscore", tmp);

                inputText.text = null;
                placeHolderText.text = "Highscore set to " + tmp;
            }
            catch
            {
                if (!(inputText.text == ""))
                {
                    inputText.text = null;
                    placeHolderText.text = "error - you have to enter a number";
                }

            }
        }
        else
        {
            if (inputText.text == code)
            {
                developer = true;

                PlayerPrefs.SetString("developer", "true");

                Debug.Log("dev");

                inputText.text = null;
                placeHolderText.text = "Set highscore";


            }
            else
            {
                inputText.text = null;
                placeHolderText.text = "Incorrect";
            }
        }



    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowYourScore : MonoBehaviour {


    public Text myText;
    bool animPlaying;
    public bool oneAtATime;

    public int fontSize1;
    public int fontSize2;

	// Use this for initialization
	void Start () {

        animPlaying = false;

	}

    public void ShowScore(string score)
    {
     


        if (!animPlaying) //checks that the animation isn't already playing
        {
            animPlaying = oneAtATime;

            Animator anim = gameObject.GetComponent<Animator>();

            if (score[0] == 'N')
            {
                myText.fontSize = fontSize2;
            }
            else
            {
                myText.fontSize = fontSize1;
            }

            myText.text = score;

            anim.Play("ShowScoreAnimation");
        }



   
    }


    public void TurnOffShowScore()
    {
        animPlaying = false;

        gameObject.SetActive(false);
    }

}

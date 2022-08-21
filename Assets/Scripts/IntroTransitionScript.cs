using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTransitionScript : MonoBehaviour {

    public GameObject g;

    public void AnimStopped()
    {
        //om jag vill att timern inte ska starta på direkten
        //ScoreScript scoreScr = g.GetComponent<ScoreScript>();
        //scoreScr.startTimer = true; 

        gameObject.SetActive(false);
    }
	

}

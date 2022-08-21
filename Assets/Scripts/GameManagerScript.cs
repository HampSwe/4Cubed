using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {


    public GameObject introTransObj;

    public GameObject outroTransObj;


	// Use this for initialization
	void Start () {

        introTransObj.SetActive(true);

        outroTransObj.SetActive(false);

	}


    public void OutroStart()
    {
        outroTransObj.SetActive(true);
    }
	

}

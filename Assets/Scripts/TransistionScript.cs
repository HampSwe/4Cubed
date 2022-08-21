using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransistionScript : MonoBehaviour {


    public GameObject g;



    void AnimFinished()
    {
        SceneManagerScript sceneScript = g.GetComponent<SceneManagerScript>();

        sceneScript.LoadNewScene();
    }


}

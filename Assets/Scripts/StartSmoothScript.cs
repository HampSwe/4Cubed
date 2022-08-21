using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSmoothScript : MonoBehaviour {


    public void AnimationFinished()
    {
        gameObject.SetActive(false);
    }


}

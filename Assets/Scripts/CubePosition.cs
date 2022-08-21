using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePosition : MonoBehaviour {


    public Transform tf;

    public float screenTime;

    public bool vertical;

    //calculates the ratio between the screen width and height
    float ratio = (float)Screen.width / (float)Screen.height;

    // Use this for initialization
    void Start () {


        Vector3 tmp = new Vector3();

        if (vertical)
        {
            tmp.Set(0, -Camera.main.orthographicSize, 5);
        }
        else
        {
            tmp.Set(ratio * -Camera.main.orthographicSize, 0, 5);
        }

        tf.position = tmp;
    
	}
	
	// Update is called once per frame
	void Update () {

        //creates a temporary vector to change the position

        Vector3 tmp2 = new Vector3();

        tmp2 = tf.position;

        if (vertical)
        {

            //calculates the speed of the object, in units per frame. This is calculated by first calculating the distance it has to travel, (using the ratio of the screen and the camera size), then dividing by the time. Finally it multiplies by deltaTime to answer in units/frame (since it was first calculated in units/seconds, and it has to be per frame since this is in the "Update" method which is called each frame)
            float ySpeed = 2 * Camera.main.orthographicSize / screenTime;

            tmp2.Set(tmp2.x, tmp2.y + ySpeed * Time.deltaTime, tmp2.z);
        }
        else
        {
            float xSpeed = ratio * 2 * Camera.main.orthographicSize / screenTime;

            tmp2.Set(tmp2.x + xSpeed * Time.deltaTime, tmp2.y, tmp2.z);
        }

        //updates the real position
        tf.position = tmp2;

    }
}

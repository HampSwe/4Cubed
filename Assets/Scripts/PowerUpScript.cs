using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    public Transform tf;
    Vector3 pos;

    public GameObject mySpawner;
    float height;
    float width;

    float xPos;
    float yPos;


    // Use this for initialization
    void Start()
    {

        height = Camera.main.orthographicSize; //calculates the screen size in units
        width = (float)Screen.width / (float)Screen.height * Camera.main.orthographicSize;

        xPos = Random.Range(-width + tf.localScale.x / 2, width - tf.localScale.x / 2); //randomizes the position;
        yPos = Random.Range(-height + tf.localScale.y / 2, height - tf.localScale.y / 2);

        pos.Set(xPos, yPos, 0); //sets the position to a Vector3

        tf.position = pos; //applies the position

        goToSide = false;

    }

    Vector3 tmp2;
    float myRotationA;
    bool goToSide;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goToSide)
        {
            tmp2 = tf.position;

            tmp2.Set(tmp2.x + xC * Time.deltaTime, tmp2.y + yC * Time.deltaTime, tmp2.z);
            myRotationA += rotationSpeed * Time.deltaTime;
            tf.rotation = Quaternion.Euler(0, 0, myRotationA);

            tf.position = tmp2;

        }
    }

    float xC;
    float yC;
    float rotationSpeed;


    public void ShieldExplosion(Vector3 expPos, float strength, float sMin, float sMax, float pIntervall, float aInterval)
    {

        float P1 = Random.Range(1 - pIntervall, 1 + pIntervall);
        float P2 = Random.Range(1 - pIntervall, 1 + pIntervall);
        float multiplier = strength * Random.Range(1 - sMin, 1 + sMax);

        float deltaX = expPos.x - tf.position.x;
        float deltaY = expPos.y - tf.position.y;
        float sumCathete = -(Mathf.Abs(deltaX) + Mathf.Abs(deltaY));

        if (!(sumCathete == 0))
        {
            xC = (deltaX / sumCathete) * multiplier * P1;
            yC = (deltaY / sumCathete) * multiplier * P2;
        }

        rotationSpeed = Random.Range(-aInterval, aInterval);

        goToSide = true;

    }
}

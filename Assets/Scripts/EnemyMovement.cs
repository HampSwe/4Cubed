using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{


    public Transform tf;
    public GameObject enemySpawner;
    SpawnEnemy sEScript;
    public float screenTime;
    public float zStart;


    public float offSet; //units outside of the screen on spawn
    float speedMultiplier;
    public float maxSpeedMultiplier;
    public float minSpeedMultiplier;

    float height;
    float width;
    float distY;
    float distX;
    float ySpeed;
    float xSpeed;
    float xC;
    float yC;
    float maxPosX;
    float maxPosY;

    float rndPercent;
    bool vertical;
    int mirror;



    // Use this for initialization
    void Start()
    {

        //calculates the screens height and width in units

        height = 2 * Camera.main.orthographicSize;
        width = (float)Screen.width / (float)Screen.height * 2 * Camera.main.orthographicSize;



        //RANDOMIZES THE POSITION VALUES OF THE ENEMY

        if (Random.Range(0, 2) == 1)
        {
            vertical = true;
        }
        else
        {
            vertical = false;
        }

        rndPercent = Random.Range(-0.5f, 0.5f);

        if (Random.Range(0, 2) == 1)
        {
            mirror = -1;
        }
        else
        {
            mirror = 1;
        }

        speedMultiplier = Random.Range(minSpeedMultiplier, maxSpeedMultiplier);


        //sets the starting position

        Vector3 tmp = new Vector3();

        if (vertical)
        {
            tmp.Set(width * rndPercent, mirror * ((height + tf.localScale.y) / 2 + offSet), zStart);
        }
        else
        {
            tmp.Set(mirror * ((width + tf.localScale.x) / 2 + offSet), height * rndPercent, zStart);
        }

        distY = height / screenTime;
        distX = width / screenTime;

        /*
        maxPosX = (width + tf.localScale.x) / 2 + offSet;
        maxPosY = (height + tf.localScale.y) / 2 + offSet;
        */

        tf.position = tmp;

        sEScript = enemySpawner.GetComponent<SpawnEnemy>();

    }


    Vector3 tmp2;


    // Update is called once per frame
    void Update()
    {

        //creates a temporary vector to change the position

        tmp2 = tf.position;



        if (sEScript.explodeToSide)
        {
            xSpeed = xC * Time.deltaTime;
            ySpeed = yC * Time.deltaTime;

            tmp2.Set(tmp2.x + xSpeed, tmp2.y + ySpeed, tmp2.z);

            myRotation += rotationSpeed * Time.deltaTime;

            tf.rotation = Quaternion.Euler(0, 0, myRotation);

        }
        else
        {

            if (vertical)
            {

                ySpeed = distY * Time.deltaTime; //calculates the speed of the object, in units per frame. This is calculated by first calculating the distance it has to travel, (using the ratio of the screen and the camera size), then dividing by the time. Finally it multiplies by deltaTime to answer in units/frame (since it was first calculated in units/seconds, and it has to be per frame since this is in the "Update" method which is called each frame)

                tmp2.Set(tmp2.x, tmp2.y + ySpeed * -mirror * speedMultiplier, tmp2.z);

            }
            else
            {

                xSpeed = distX * Time.deltaTime;

                tmp2.Set(tmp2.x + xSpeed * -mirror * speedMultiplier, tmp2.y, tmp2.z);

            }
        }


        //updates the real position
        tf.position = tmp2;

    }

    //FIX: TOO MANY CALCULATIONS IN UPDATE SCRIPT, MAY CAUSE LAG



    public float rotationSpeed;
    float myRotation;


    public void EnemyExplosion(Vector3 expPos, float strength, float sMin, float sMax, float pIntervall, float aInterval)
    {

        //stop other scripts

        float P1 = Random.Range(1 - pIntervall, 1 + pIntervall);
        float P2 = Random.Range(1 - pIntervall, 1 + pIntervall);
        float multiplier = strength * Random.Range(1 - sMin, 1 + sMax);

        float deltaX = expPos.x - tf.position.x;
        float deltaY = expPos.y - tf.position.y;
        float sumCathete = -(Mathf.Abs(deltaX) + Mathf.Abs(deltaY));

        xC = (deltaX / sumCathete) * multiplier * P1;
        yC = (deltaY / sumCathete) * multiplier * P2;

        rotationSpeed = Random.Range(-aInterval, aInterval);


    }

}

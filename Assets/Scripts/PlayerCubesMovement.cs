using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubesMovement : MonoBehaviour
{

    public Transform controlTf;
    public Transform myTf;
    public GameObject g;
    public GameObject enemySpawner;
    public GameObject score;
    SpawnEnemy se;

    float myOffSet;
    bool myRotationSystem;

    public bool up;
    public bool left;

    public bool mouseMovement = true;

    public bool cubesBackFirst = true;

    public bool hasShield = false;

    void Start()
    {
        mouseMovement = true;

        MovementScript e = g.GetComponent<MovementScript>();
        myOffSet = e.offSet;
        se = enemySpawner.GetComponent<SpawnEnemy>();

        myTimer = 0;
        cubesBackFirst = true;

        RemoveShield();
        pUSpawnScript.appliedShields = 0;


        /*
        mouseMovement = true;
        myTimer = 0;

        myTf.position = getPosition(controlTf);
        */
    }


    Vector3 tmp2;
    float xC;
    float yC;
    float myRotationA;
    float rotationSpeed;


    public void PlayerExplosion(Vector3 expPos, float strength, float sMin, float sMax, float pIntervall, float aInterval)
    {

        mouseMovement = false;

        float P1 = Random.Range(1 - pIntervall, 1 + pIntervall);
        float P2 = Random.Range(1 - pIntervall, 1 + pIntervall);
        float multiplier = strength * Random.Range(1 - sMin, 1 + sMax);

        float deltaX = expPos.x - myTf.position.x;
        float deltaY = expPos.y - myTf.position.y;
        float sumCathete = -(Mathf.Abs(deltaX) + Mathf.Abs(deltaY));

        if (!(sumCathete == 0))
        {
            xC = (deltaX / sumCathete) * multiplier * P1;
            yC = (deltaY / sumCathete) * multiplier * P2;
        }

        rotationSpeed = Random.Range(-aInterval, aInterval);

    }

    public float myTimer = 0;
    public float timeBack;
    float rotSpeed;
    float myRotZ;
    Vector3 aimPosition;

    void FixedUpdate()
    {
        if (se.cubesBack) //the script to smoothly get back to the mouse
        {

            myTimer += Time.fixedDeltaTime;

            if (myTimer < timeBack)
            {
                if (cubesBackFirst)
                {
                    myTimer = 0;
                    cubesBackFirst = false;
                    myTf.position = RandomizePosition();
                    myRotZ = Random.Range(-360f, 360f);
                    myTf.rotation = Quaternion.Euler(0, 0, myRotZ);
                    rotSpeed = myRotZ / ((timeBack + Time.fixedDeltaTime) / Time.fixedDeltaTime);
                }

                aimPosition = getPosition(controlTf);

                float dX = aimPosition.x - myTf.position.x;
                float dY = aimPosition.y - myTf.position.y;

                float xMove = (dX / (timeBack + Time.fixedDeltaTime - myTimer)) * Time.fixedDeltaTime;
                float yMove = (dY / (timeBack - myTimer)) * Time.fixedDeltaTime;

                myRotZ -= rotSpeed;

                Vector3 tmp = new Vector3();
                tmp.Set(myTf.position.x + xMove, myTf.position.y + yMove, 0);

                myTf.rotation = Quaternion.Euler(0, 0, myRotZ);
                myTf.position = tmp;

            }
            else
            {
                se.ResetGame(); //starts a new game
            }
       

        }
        else if (se.explodeToSide)
        {
            tmp2 = myTf.position;

            tmp2.Set(tmp2.x + xC * Time.deltaTime, tmp2.y + yC * Time.deltaTime, tmp2.z);
            myRotationA += rotationSpeed * Time.deltaTime;
            myTf.rotation = Quaternion.Euler(0, 0, myRotationA);

            myTf.position = tmp2;

        }
        else if (mouseMovement && gameObject.activeSelf)
        {
                myTf.position = getPosition(controlTf);
        }

    }

    public Material playerMat;
    public Material shieldMat;

    public PowerUpSpawnScript pUSpawnScript;

    public void ApplyShield()
    {
        hasShield = true;

        gameObject.GetComponent<MeshRenderer>().material = shieldMat;

        pUSpawnScript.appliedShields++;
    }

    public void RemoveShield()
    {
        hasShield = false;
        

        gameObject.GetComponent<MeshRenderer>().material = playerMat;

        //pUSpawnScript.appliedShields--;
    }



    public Vector3 RandomizePosition() //gets a random position of the screen border
    {
        //calculates the screens height and width in units

        float height = 2 * Camera.main.orthographicSize;
        float width = (float)Screen.width / (float)Screen.height * 2 * Camera.main.orthographicSize;

        float rndPercent = Random.Range(-0.5f, 0.5f);
        float mirror;

        if (Random.Range(0, 2) == 1)
        {
            mirror = -1;
        }
        else
        {
            mirror = 1;
        }

        Vector3 tmp = new Vector3();

        if (Random.Range(0, 2) == 1)
        {
            tmp.Set(width * rndPercent, mirror * ((height + myTf.localScale.y) / 2), 0);
        }
        else
        {
            tmp.Set(mirror * ((width + myTf.localScale.x) / 2), height * rndPercent, 0);
        }

        return tmp;
    }






    Vector3 getPosition(Transform tf)
    {
        Vector3 tmp = new Vector3();

        if (up)
        {
            if (left)
            {
                tmp.Set(tf.position.x - myOffSet, tf.position.y + myOffSet, tf.position.z);
            }
            else
            {
                tmp.Set(tf.position.x + myOffSet, tf.position.y + myOffSet, tf.position.z);
            }
        }
        else
        {
            if (left)
            {
                tmp.Set(tf.position.x - myOffSet, tf.position.y - myOffSet, tf.position.z);
            }
            else
            {
                tmp.Set(tf.position.x + myOffSet, tf.position.y - myOffSet, tf.position.z);
            }
        }

        return tmp;
    }









    //ALL THE CODE BELOW IS RELATED TO THE DIFFERENT POSITION-SYSTEM WITH ROTATION


    public float myStartRotation;
    float myRotation;
    float myDistance;


    void RotationSystem() //Another system that let's the user rotate the cube
    {
        MovementScript e = g.GetComponent<MovementScript>(); //needs this component to get the propterites from the PlayerController

        myRotation = e.rotation; //loads the values
        myDistance = myOffSet * Mathf.Sqrt(2);

        Quaternion tmpQuaternion = Quaternion.Euler(0, 0, myRotation + myStartRotation); //creates a quaternion, and rotates around the z-axis


        myTf.position = getPos2(controlTf, myRotation + myStartRotation - 45, myDistance); //gets the postion of the cube, and applies it. All cubes have a different startRotation, which positions them in different corners of a big "square".
        myTf.rotation = tmpQuaternion; //applies the rotaion
    }


    Vector3 getPos2(Transform tf, float rotation, float distance) //a method for finding the position of a rotated cube
    {
        Vector3 tmp = new Vector3(); //a temporary vector for storing values

        tmp.Set(tf.position.x + Mathf.Cos(rotation * Mathf.Deg2Rad) * distance, tf.position.y + Mathf.Sin(rotation * Mathf.Deg2Rad) * distance, tf.position.z); //uses trigonmetry to find the cubes position, by the angle it has rotated.

        return tmp;
    }

}

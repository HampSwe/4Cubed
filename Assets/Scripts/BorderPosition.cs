using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPosition : MonoBehaviour
{

    public float offSet;
    public int side;
    public float scaleX;
    public float scaleY;

    public Transform enemyTf;
    public Transform myTf;
    public BoxCollider box;
    public GameObject PlayerController;
    public Transform PlayerTransform;

    float originalHeight = 10f;
    float originalWidth = 10f;

    // Use this for initialization
    void Start()
    {

        float height = 2 * Camera.main.orthographicSize; //calculates the heigt of the screen in units
        float width = (float)Screen.width / (float)Screen.height * height; //calculates the width of the screen in units

        float centerCubeDis = PlayerController.GetComponent<MovementScript>().offSet * 2 - PlayerTransform.localScale.x;

        float enemyDiagonal = Mathf.Sqrt(enemyTf.localScale.x * enemyTf.localScale.x + enemyTf.localScale.y * enemyTf.localScale.y); //calculates the diagonal of the enemy, to make sure that the entire enemy is outside even if it's rotated
        float posX = (width + scaleY) / 2 + offSet + enemyDiagonal + centerCubeDis; //calculates the x-position
        float posY = (height + scaleY) / 2 + offSet + enemyDiagonal + centerCubeDis; //calculates the y-position

        Vector3 posTmp = new Vector3();
        Quaternion rotTmp = new Quaternion();

        if (side == 1) //checks which side the current border is placed at
        {
            posTmp.Set(posX, 0, 0); //stores the proper values for this side to temporary variables
            rotTmp = Quaternion.Euler(0, 0, 90);
            scaleX = height / originalHeight * scaleX;
            box.size = new Vector3(posY * 2, scaleY, 2);
        }
        else if (side == 2)
        {
            posTmp.Set(0, posY, 0);
            rotTmp = Quaternion.Euler(0, 0, 180);
            scaleX = width / originalWidth * scaleX;
            box.size = new Vector3(posX * 2, scaleY, 2);
        }
        else if (side == 3)
        {
            posTmp.Set(-posX, 0, 0);
            rotTmp = Quaternion.Euler(0, 0, -90);
            scaleX = height / originalHeight * scaleX;
            box.size = new Vector3(posY * 2, scaleY, 2);
        }
        else
        {
            posTmp.Set(0, -posY, 0);
            rotTmp = Quaternion.Euler(0, 0, 0);
            scaleX = width / originalWidth * scaleX;
            box.size = new Vector3(posX * 2, scaleY, 2);
        }

        myTf.position = posTmp; //applies the position
        myTf.rotation = rotTmp; //applies the rotation

        sEs = sE.GetComponent<SpawnEnemy>();

    }


    public GameObject sE;
    SpawnEnemy sEs;
    public PowerUpSpawnScript pUScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            sEs.ProperDestroy(other.gameObject); //if it collides with an enemy, it destroys it "properly"
        }
        else if (other.gameObject.tag == "Player" && sEs.explodeToSide) //checks if the player-cube hits when it's exploding
        {
            sEs.CubesHit += 1;
            other.gameObject.GetComponent<PlayerCubesMovement>().RemoveShield();
            //other.gameObject.GetComponent<Renderer>().enabled = false;  //turns off the player-cube and registers the hit
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "PowerUp" && sEs.explodeToSide)
        {
            pUScript.ProperDestroy(other.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHit : MonoBehaviour {


    public Material normalMat;
    public Material blinkMat;

    public GameObject playerCont;
    public GameObject sEObj;
    public GameObject pUSpawner;
    public GameObject shield;

    public Transform myTf;

    float myBlinkDuration;
    float repeat;

    public GameObject score;

    private void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = normalMat;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            SpawnEnemy sE = sEObj.GetComponent<SpawnEnemy>();

            if (sE.spawn)
            {
                if (gameObject.GetComponent<PlayerCubesMovement>().hasShield)
                {
                    if (pUSpawner.GetComponent<PowerUpSpawnScript>().instancePUList.Count + pUSpawner.GetComponent<PowerUpSpawnScript>().appliedShields >= 4)
                    {
                        pUSpawner.GetComponent<PowerUpSpawnScript>().timer = 0;
                    }

                    pUSpawner.GetComponent<PowerUpSpawnScript>().appliedShields--;
                    gameObject.GetComponent<PlayerCubesMovement>().RemoveShield();
                    sE.ProperDestroy(other.gameObject);
                }
                else
                {

                    ScoreScript s = score.GetComponent<ScoreScript>();
                    s.ResetScore();

                    sE.ProperDestroy(other.gameObject);
                    sE.MainExplosion(myTf.position);

                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(100, 100, 0);

                    //Blink();
                }
            }
        }
        else if (other.gameObject.tag == "PowerUp")
        {
            PowerUpSpawnScript sPU = pUSpawner.GetComponent<PowerUpSpawnScript>();

            if (sPU.se.spawn && !gameObject.gameObject.GetComponent<PlayerCubesMovement>().hasShield)
            {
                
                sPU.ProperDestroy(other.gameObject);

                /* 
                GameObject instance2 = Instantiate(shield);
                instance2.GetComponent<ShieldScript>().myPlayer = this.gameObject;
                */

                gameObject.GetComponent<PlayerCubesMovement>().ApplyShield();

                PlayerPrefs.SetInt("pUCollected", PlayerPrefs.GetInt("pUCollected") + 1);

                //Blink();
            }
        }

    }


    void Blink()
    {

        MovementScript e = playerCont.GetComponent<MovementScript>();

        myBlinkDuration = e.blinkDuration;
        repeat = e.blinkRepeat;

        


        ChangeBlinkMat();
     
        for (int i = 1; i < (repeat * 2); i += 2)
        {
            Invoke("ChangeNormalMat", myBlinkDuration * i);

            Invoke("ChangeBlinkMat", myBlinkDuration * (i + 1));
        }


        Invoke("ChangeNormalMat", myBlinkDuration * (repeat * 2));


    }

    void ChangeNormalMat()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = normalMat;
    }

    void ChangeBlinkMat()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = blinkMat;
    }

}

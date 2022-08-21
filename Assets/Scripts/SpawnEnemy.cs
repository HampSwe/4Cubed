using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{


    public GameObject enemy;
    public GameObject score;

    float timer;
    public float spawnRate;

    public float expStrength;
    public float expStrengthMin;
    public float expStrengthMax;
    public float pIntervall;
    public float angleIntervall;


    public bool spawn = true;
    public bool explodeToSide = false;
    public bool cubesBack = false;
    public bool firstCubesHit = true;

    public List<GameObject> instanceList = new List<GameObject>();
    public GameObject[] playerCubes = new GameObject[4];
    public int CubesHit = 0;

    private void Start()
    {
        spawn = true;
        explodeToSide = false;
        cubesBack = false;
        CubesHit = 0;
        firstCubesHit = true;
        pUS.appliedShields = 0;

    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= spawnRate && spawn)
        {
            GameObject instance = Instantiate(enemy);

            instanceList.Add(instance);

            instance.GetComponent<EnemyMovement>().enemySpawner = gameObject;

            timer = 0;
        }

        if (CubesHit == 3 && instanceList.Count == 0 && firstCubesHit && pUS.instancePUList.Count == 0) //AND ALL ENEMIES ARE GONE, CHECK LIST
        {
            firstCubesHit = false;

            Invoke("GetCubesBack", waitTime);
        }

    }

    public float waitTime;

    public ScoreScript scoreScript;

    void GetCubesBack()
    {
        scoreScript.updateMyColor();

        foreach (GameObject item in playerCubes)
        {
            item.GetComponent<PlayerCubesMovement>().mouseMovement = false;
        }

        cubesBack = true;
        explodeToSide = false;

        foreach (GameObject item in playerCubes)
        {
            item.SetActive(true);
        }
    }

    public PowerUpSpawnScript pUS;

    public void ResetGame() //starts a new game
    {

        firstCubesHit = true;
        cubesBack = false;
        spawn = true;
        CubesHit = 0;
        pUS.timer = 0;
        pUS.appliedShields = 0;
        


        foreach (GameObject item in playerCubes)
        {
            item.GetComponent<PlayerCubesMovement>().mouseMovement = true;
            item.GetComponent<PlayerCubesMovement>().myTimer = 0;
            item.GetComponent<PlayerCubesMovement>().cubesBackFirst = true;
        }

        score.GetComponent<ScoreScript>().startTimer = true;
        score.GetComponent<ScoreScript>().scoreText.text = "0.0";
    }


    public void ProperDestroy(GameObject obj)
    {
        instanceList.Remove(obj);
        Destroy(obj);
    }


    public void MainExplosion(Vector3 pos)
    {
        spawn = false;
        explodeToSide = true;
        score.GetComponent<ScoreScript>().startTimer = false;

        foreach (GameObject item in instanceList)
        {
            item.GetComponent<EnemyMovement>().EnemyExplosion(pos, expStrength, expStrengthMin, expStrengthMax, pIntervall, angleIntervall);
        }

        foreach (GameObject item in playerCubes)
        {
            item.GetComponent<PlayerCubesMovement>().PlayerExplosion(pos, expStrength, expStrengthMin, expStrengthMax, pIntervall, angleIntervall);
        }

        foreach (GameObject item in pUS.instancePUList)
        {
            item.GetComponent<PowerUpScript>().ShieldExplosion(pos, expStrength, expStrengthMin, expStrengthMax, pIntervall, angleIntervall);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnScript : MonoBehaviour {


    public float timer;
    float nextSpawn;

    public float secIntervall;
    public float spawnRate;
    public SpawnEnemy se;

    public GameObject powerUp;
    public List<GameObject> instancePUList = new List<GameObject>();
    public float appliedShields;


    // Use this for initialization
    void Start () {

        timer = 0;
        appliedShields = 0;

        nextSpawn = spawnRate + Random.Range(-secIntervall, secIntervall);

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= nextSpawn && se.spawn && ((instancePUList.Count + appliedShields) < 4))
        {
            GameObject instance = Instantiate(powerUp);
            instance.GetComponent<PowerUpScript>().mySpawner = gameObject;

            instancePUList.Add(instance);

            nextSpawn = spawnRate + Random.Range(-secIntervall, secIntervall);
            timer = 0;
        }


    }


    public void ProperDestroy(GameObject obj)
    {
        instancePUList.Remove(obj);
        Destroy(obj);
    }
}

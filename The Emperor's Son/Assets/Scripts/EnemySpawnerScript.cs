using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject hppowerup;
    public GameObject regenhppowerup;
    public GameObject jumppowerup;
    public GameObject speedpowerup;
    public int enemyCount;
    int randX;
    int randY;
    int randomspawn;
    int randompower;
    Vector2 whereToSpawn;
    public float spawnRate = 4f;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            
            if (enemyCount % 4 == 0 && enemyCount != 0)
                ScoreScript.scoreValue += 30;
            if (enemyCount % 5 == 0 && enemyCount != 0 && spawnRate > 1.5f)
                spawnRate -= 0.3f;
            if (enemyCount %6 == 0 && enemyCount != 0)
            {
                randX = Random.Range(-93, -78);
                whereToSpawn = new Vector2(randX, -3);
                randompower = Random.Range(1, 2000);
                if(randompower % 4 == 0)
                Instantiate(hppowerup, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform);
                if (randompower % 4 == 1)
                Instantiate(speedpowerup, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform);
                if (randompower % 4 == 2)
                Instantiate(regenhppowerup, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform);
                if (randompower % 4 == 3)
                Instantiate(jumppowerup, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform);
            }
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-18, 20);
            randY = Random.Range(2, 12);
            whereToSpawn = new Vector2(randX, randY);
            randomspawn= Random.Range(1, 2000);
            if (randomspawn % 3 == 0)
            Instantiate(enemy, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform); ;
            if (randomspawn % 3 == 1)
                Instantiate(enemy2, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform); ;
            if (randomspawn % 3 == 2)
                Instantiate(enemy3, whereToSpawn, Quaternion.identity).transform.SetParent(GameObject.Find("Interactables").transform); ;
            enemyCount++;
        }
    }
}

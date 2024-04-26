using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{


    //NOTE: In new spawn point system, the instantiated enemies should be added to an array. When the enemy object is destroyed
    //i assume it will be removed from the array. when the array is completely empty, the script can spawn enemies again
    //note: destroying a gameobject does not remove it from an array

    //options:
    //loop through array every update and remove null values - flaws: numerous executions, could be expensive (theoretically)
    //pass SpawnEnemies script reference to enemies with their position in the array. enemy removes itself from the array upn death - flaws: additional variables passed into enemy, adding yet another script reference



    //public GameObject[] existingEnemies;
    public List<GameObject> enemies = new List<GameObject>();

    public GameObject enemy;
    public GameObject curEnemy;

    public int maxEnemies;

    public float timeBetweenSpawn;

    float spawnCountdown;

    public float spawnRange;

    public GameObject player;

    void Start()
    {
        spawnCountdown = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log(enemies[i]);
            if (enemies[i] == null)
            {
                Debug.Log("BEGONE");
                enemies.RemoveAt(i);
            }
        }

        if (enemies.Count < maxEnemies)
        {
            //if player.transform.position.x == this.transform.position.x+spawnRange
            if (spawnCountdown <= 0)
            {
                Debug.Log("Instantiate");
                Spawn();
                    
            }
                

        }
    
        spawnCountdown -= Time.deltaTime;
    }


    void Spawn()
    {
        spawnCountdown = timeBetweenSpawn;

        enemies.Add(Instantiate(enemy, this.transform.position, transform.rotation));

        Debug.Log(enemies[enemies.Count-1]);

    }
}

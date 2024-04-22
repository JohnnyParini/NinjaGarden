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



    public GameObject[] existingEnemies;
    public List<GameObject> enemies;

    public GameObject enemy;
    public GameObject curEnemy;

    public int maxEnemies;

    public enum spawnState { spawning, waiting, counting };


    public float timeBetweenSpawn;

    public float spawnCountdown;

    public float searchCountdown;

    //public Transform spawnCoords = Transform.posit

    public spawnState State = spawnState.counting;

    void Start()
    {
        spawnCountdown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] = null)
            {
                enemies.RemoveAt(i);
            }
        }

        if (State == spawnState.waiting)
        {
            if (enemies.Count < maxEnemies)
            {
                Debug.Log("More enemies can be spawned");

                if (spawnCountdown <= 0)
                {
                    if (State != spawnState.spawning)
                    {
                        Spawn();
                    }
                   
                }
                

            }

            else
            {
                return;
            }
        }

        spawnCountdown -= Time.deltaTime;

        
    }


    void Spawn()
    {
        spawnCountdown = timeBetweenSpawn;

        curEnemy = Instantiate(enemy, this.transform.position, transform.rotation);

        enemies.Add(curEnemy);

        State = spawnState.waiting;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{


    //NOTE: In new spawn point system, the instantiated enemies should be added to an array. When the enemy object is destroyed
    //i assume it will be removed from the array. when the array is completely empty, the script can spawn enemies again
    // Start is called before the first frame update

    public GameObject[] existingEnemies;

    public enum spawnState { spawning, waiting, counting };

    public float timeBetweenSpawn = 1f;

    public float searchCountdown = 1f;

    public spawnState State = spawnState.counting;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (State == spawnState.waiting)
        {
            if (existingEnemies.Length == 0)
            {
                Debug.Log("No active enemies");
            }

            else
            {
                return;
            }
        }
    }
}

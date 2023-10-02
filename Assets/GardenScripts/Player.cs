using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public inventory Inventory;
    

    private void Awake()
    {
        Inventory = new inventory(32);
     
    }

    public void DropItem(collectable item)
    {
        Vector3 spawnLocation = transform.position; //the location of the player when the player drops the item

        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);

        Vector3 spawnOffset = new Vector3(randX, randY, 0f).normalized;
         
    }

}

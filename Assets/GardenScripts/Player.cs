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

    public void Update()
    {
        
       //need to get the count variable from inventory and update it. 
       //never mind, I fixed it
    }

    public void DropItem(collectable item)
    {
        Vector3 spawnLocation = transform.position; //the location of the player when the player drops the item

        Vector3 spawnOffset = Random.insideUnitCircle * 10.25f; //so it doesn't spawn on the player, make an offset

        collectable droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity); //spawn at location + offset

        droppedItem.rb2d.AddForce(spawnOffset * .8f, ForceMode2D.Impulse); //allows it to slide with instantananeous force
         
    }

}

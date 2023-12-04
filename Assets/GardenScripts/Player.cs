using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public InventoryManager inventory;


    
    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //if space bar is pressed
        {
            Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0); //coverts the player position to an int, not a float. 

            if (GameManager.instance.tileManager.IsInteractable(position))
            {
                Debug.Log("Tile is Interactable");
                GameManager.instance.tileManager.SetInteracted(position); //set the tile to a plowed tile
            }
        }
    }

    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position; //the location of the player when the player drops the item

        Vector3 spawnOffset = Random.insideUnitCircle * 5.75f; //so it doesn't spawn on the player, make an offset

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity); //spawn at location + offset

        droppedItem.rb2d.AddForce(spawnOffset * .8f, ForceMode2D.Impulse); //allows it to slide with instantananeous force
         
    }
    public void DropItem(Item item, int NumToDrop)
    {
       for(int i = 0; i < NumToDrop; i++)
        {
            DropItem(item);
        }

    }

}

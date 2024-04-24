using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    public InventoryManager inventory;

    private TileManager tileManager;

    private Toolbar_UI toolbarUI;

    public Vector3 mousePos;

    public Vector3 ResetVector;
    
    private void Awake()
    {
        
        //DontDestroyOnLoad(this);
        inventory = GetComponent<InventoryManager>();
        tileManager = GetComponent<TileManager>();
        toolbarUI = GetComponent<Toolbar_UI>();
    }

    public void setAwake()
    {
        Awake();
    }

    public void ResetPosition()
    {
        //transform.SetPositionandRotation(ResetVector, 0);
        
    }

    public void Update()
    {
      
      
        if (Input.GetKeyDown(KeyCode.Space)) //if space bar is pressed
        {
            if (tileManager != null)
            {
                Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0); //coverts the player position to an int, not a float. 

                string tileName = tileManager.GetTileName(position);
               
                

                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    //if we get something back that isn't empty:
                    if(tileName == "Interactable_Invis" && inventory.toolbar.selectedSlot.itemName == "FarmingStaff") //&& if holding the farming staff
                    {
                        tileManager.SetInteracted(position);
                        Debug.Log("Truly a coding savant");
                    }
                }

                //if (GameManager.instance.tileManager.IsInteractable(position))
                //{
                 //   Debug.Log("Tile is Interactable");
                  //  GameManager.instance.tileManager.SetInteracted(position); //set the tile to a plowed tile
                //}
            }
            
        }
    }

    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position; //the location of the player when the player drops the item

        Vector3 spawnOffset = Random.insideUnitCircle * 6.25f; //so it doesn't spawn on the player, make an offset

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

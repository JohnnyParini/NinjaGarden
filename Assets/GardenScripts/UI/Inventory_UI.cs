using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;
    public Movement move;

    public Animator animate;

    public List<Slot_UI> slots = new List<Slot_UI>();
    void Start()
    {
        ToggleInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //if the player hits the tab button
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf) //if inventory panel is turned off
        {
            inventoryPanel.SetActive(true); //toggle it on
            player.GetComponent<Movement>().enabled = false; //Disables movement 
            animate.SetBool("isMoving", false);
            Refresh();    
        }
        else //if inventory panel is turned on
        {
            inventoryPanel.SetActive(false); //toggle it off
            player.GetComponent<Movement>().enabled = true; //enables movement
            //move.animator.SetBool("isMoving", true);
        }
    }

    public void Refresh()
    {
        Debug.Log(player.Inventory.slots.Count + "player slots");
        Debug.Log(slots.Count + "Slots");
        if(slots.Count == player.Inventory.slots.Count) //if the player and the slots have the same number
        {
            Debug.Log("TRUE TRUE TRUE");
            for(int i = 0; i<slots.Count; i++)
            {
                Debug.Log("iteration " + i);
                Debug.Log("slot " + i + " type = " + player.Inventory.slots[i].type);
                if(player.Inventory.slots[i].type != CollectableType.NONE) //if the slot isn't NONE
                    
                {
                    slots[i].SetItem(player.Inventory.slots[i]);
                    Debug.Log("Slot " + i + " is occupied");
                }
                else
                {
                    slots[i].SetEmpty();
                    Debug.Log("Setting empty slot " + i);
                }

            }
        }
        else
        {
            Debug.Log("Your code sucks idiot");
        }
    }

    public void Remove(int SlotID)
    {
        collectable itemToDrop = GameManager.instance.itemManager.GetItemByType(player.Inventory.slots[SlotID].type); //gets the type of the item we are removing
        
        if(itemToDrop != null)
        {

            player.DropItem(itemToDrop); //drops item
            player.Inventory.Remove(SlotID); //removes item
            Refresh(); //refreshes inventory
        }
        
    }


}

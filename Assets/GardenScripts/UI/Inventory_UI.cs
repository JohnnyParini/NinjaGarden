using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;

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
            Setup();    
        }
        else //if inventory panel is turned on
        {
            inventoryPanel.SetActive(false); //toggle it off
        }
    }

    public void Setup()
    {
        if(slots.Count == player.Inventory.slots.Count)
        {
            for(int i = 0; i<slots.Count; i++)
            {
                if(player.Inventory.slots[i].type != CollectableType.NONE) //if the type isn't NONE
                {

                }
            }
        }
    }

}

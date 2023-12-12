using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> InventoryUIByName = new Dictionary<string, Inventory_UI>();

    public List<Inventory_UI> inventory_UIs;

    public static Slot_UI draggedSlot;
    public static Image draggedIcon;

    public static bool dragAll;

    public GameObject inventoryPanel;

    public GameObject player;

    public Animator animate; 

    private void Awake()
    {
        Initialize();
        ToggleInventoryUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I ))
        {
            ToggleInventoryUI();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragAll = true;

        }
        else
        {
            dragAll = false;

        }
    }

    public void ToggleInventoryUI() //turns on and off the inventory
    {
        if (inventoryPanel != null) //MAKE SURE TO LEAVE THE INVENTORY PANEL NULL FOR THE TOOLBAR //makes sure only the actual inventory toggles
        {



            if (!inventoryPanel.activeSelf) //if inventory panel is turned off
            {
                inventoryPanel.SetActive(true); //toggle it on
                player.GetComponent<Movement>().enabled = false; //Disables movement 
                animate.SetBool("isMoving", false);
                RefreshInventory("Backpack");
            }
            else //if inventory panel is turned on
            {
                inventoryPanel.SetActive(false); //toggle it off
                player.GetComponent<Movement>().enabled = true; //enables movement
                                                                //move.animator.SetBool("isMoving", true);
            }
        }
    }

    public void RefreshInventory(string inventoryName)
    {
        if (InventoryUIByName.ContainsKey(inventoryName))
        {
            InventoryUIByName[inventoryName].Refresh();
        }
    }
    public void RefreshAll()
    {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in InventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }


    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if (InventoryUIByName.ContainsKey(inventoryName))
        {
            return InventoryUIByName[inventoryName];
        }
        Debug.LogWarning("There is no inventory UI for " + inventoryName);
        return null;
    }

    public void Initialize()
    {
        foreach(Inventory_UI ui in inventory_UIs)
        {
            if (!InventoryUIByName.ContainsKey(ui.inventoryName)) //if it doesn't already contain the name
            {
                InventoryUIByName.Add(ui.inventoryName, ui); //add it
            }
        }
    }
}

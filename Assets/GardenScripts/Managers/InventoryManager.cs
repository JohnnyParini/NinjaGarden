using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //allows for the transfer of objects between inventories, ex: inventory and toolbar. potential shopping system?

    public Dictionary<string, inventory> inventoryByName = new Dictionary<string, inventory>();

    [Header("Backpack")]
    public inventory backpack;
    public int backpackSlotsCount;

    [Header("Toolbar")]
    public inventory toolbar;
    public int toolbarSlotsCount;

    private void Awake()
    {
        backpack = new inventory(backpackSlotsCount);
        toolbar = new inventory(toolbarSlotsCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public void Add(string inventoryName, Item item) //allows for collectable to call this, which calls the original add function
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);
        }
    }


    public inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return (inventoryByName[inventoryName]);
        }
        Debug.Log("Inventory that your are trying to access it null");
        return null; 

    }


}

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

    private void Awake()
    {
        Initialize();
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
                InventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}

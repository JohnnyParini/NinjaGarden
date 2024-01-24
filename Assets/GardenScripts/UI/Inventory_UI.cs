using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    

    public GameObject player;
    public string inventoryName;

    public Movement move;

    public Animator animate;

    public List<Slot_UI> slots = new List<Slot_UI>();

    private Canvas canvas;

   

    private bool dragAll;

    private inventory Inventory;
   
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    public void setAwake()
    {
        Awake();
    }

    void Start()
    {
        Inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        SetUpSlots();
        //ToggleInventory();
        Refresh();
    }

    

   

    public void Refresh()
    {
        Debug.Log(Inventory.slots.Count + "player slots");
        Debug.Log(slots.Count + "Slots");
        if(slots.Count == Inventory.slots.Count) //if the player and the slots have the same number
        {
            Debug.Log("TRUE TRUE TRUE");
            for(int i = 0; i<slots.Count; i++)
            {
                //Debug.Log("iteration " + i + " of" + Inventory.slots.Count);
                //Debug.Log("slot " + i + " type = " + Inventory.slots[i].itemName);
                if(Inventory.slots[i].itemName != "") //if the slot isn't an empty string
                    
                {
                    slots[i].SetItem(Inventory.slots[i]);
                    Debug.Log("Slot " + i + " is occupied");
                }
                else
                {
                    //Debug.Log("Slot " + i + " is already empty");
                    slots[i].SetEmpty();
                    //Debug.Log("Setting empty slot " + i);
                }

            }
        }
      
    }

    public void Remove()
    {

        Debug.Log("Inventory_UI remove has been called");
        
        
            Item itemToDrop = GameManager.instance.itemManager.GetItemByName(Inventory.slots[UI_Manager.draggedSlot.SlotID].itemName); //gets the type of the item we are removing (problem here, everything but the turnip is null)
            //some problem in the tilling area. 
            //Debug.Log(itemToDrop.ToString()); //doesn't work

            if (itemToDrop != null)
            {
                Debug.Log("called item isn't null");
                if (UI_Manager.dragAll == true)
                {
                    GameManager.instance.player.DropItem(itemToDrop, Inventory.slots[UI_Manager.draggedSlot.SlotID].numInSlot); //drops all items
                    Inventory.Remove(UI_Manager.draggedSlot.SlotID, Inventory.slots[UI_Manager.draggedSlot.SlotID].numInSlot); //removes all items
                }
                else
                {
                    GameManager.instance.player.DropItem(itemToDrop); //drops a single item
                    Inventory.Remove(UI_Manager.draggedSlot.SlotID); //removes a single item
                }

                Refresh(); //refreshes inventory
            }
        
        UI_Manager.draggedSlot = null;

    }


    public void SlotBeginDrag(Slot_UI slot)
    {
        //get slotID somewhere here
        UI_Manager.draggedSlot = slot;
        
        UI_Manager.draggedIcon = Instantiate(UI_Manager.draggedSlot.itemIcon);
        UI_Manager.draggedIcon.raycastTarget = false; //so that it doesn't interfere with drag and drop

        UI_Manager.draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
        UI_Manager.draggedIcon.transform.SetParent(canvas.transform); //makes the icon a child of the canvas so that it shows up properly
       
       

        MoveToMousePosition(UI_Manager.draggedIcon.gameObject);
        Debug.Log("Start Drag: " + UI_Manager.draggedSlot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(UI_Manager.draggedIcon.gameObject);
        Debug.Log("Dragging: " + UI_Manager.draggedSlot.name);
    }

    public void SlotEndDrag()
    {
        Destroy(UI_Manager.draggedIcon.gameObject);
        UI_Manager.draggedIcon = null;


        //Debug.Log("Done Dragging: " + draggedSlot.name);
    }

    public void SlotDrop(Slot_UI slot)
    {
        //Debug.Log("Dropping:" + draggedSlot.name + " on " + slot.name); //drags draggedSlot onto Slot
        if (UI_Manager.dragAll)
        {
            UI_Manager.draggedSlot.Inventory.MoveSlot(UI_Manager.draggedSlot.SlotID, slot.SlotID, slot.Inventory, UI_Manager.draggedSlot.Inventory.slots[UI_Manager.draggedSlot.SlotID].numInSlot);
            //moves the whole stack when dragAll is true
        }
        else
        {
            UI_Manager.draggedSlot.Inventory.MoveSlot(UI_Manager.draggedSlot.SlotID, slot.SlotID, slot.Inventory);
        }

            GameManager.instance.uiManager.RefreshAll(); //refreshes everything
        
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position); //null is the camera

            toMove.transform.position = canvas.transform.TransformPoint(position); //moves the icon to mouse position

           
        }
    }

    private void SetUpSlots() //automatically sets the slotID so I dont need to manually do it
    {
        int counter = 0;
        foreach(Slot_UI slot in slots)
        {
            slot.SlotID = counter;
            counter++;
            slot.Inventory = Inventory;
        }
    }

  
}


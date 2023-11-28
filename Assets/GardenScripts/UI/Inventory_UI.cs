using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;
    public Movement move;

    public Animator animate;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    private Slot_UI draggedSlot;
    private Image draggedIcon;

   
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

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

    public void ToggleInventory() //turns on and off the inventory
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
            //Debug.Log("TRUE TRUE TRUE");
            for(int i = 0; i<slots.Count; i++)
            {
                Debug.Log("iteration " + i + " of" + player.Inventory.slots.Count);
                Debug.Log("slot " + i + " type = " + player.Inventory.slots[i].itemName);
                if(player.Inventory.slots[i].itemName != "") //if the slot isn't an empty string
                    
                {
                    slots[i].SetItem(player.Inventory.slots[i]);
                    Debug.Log("Slot " + i + " is occupied");
                }
                else
                {
                    Debug.Log("Slot " + i + " is already empty");
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
        Debug.Log("Inventory_UI remove has been called");
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(player.Inventory.slots[SlotID].itemName); //gets the type of the item we are removing (problem here, everything but the turnip is null)
        //Debug.Log(itemToDrop.ToString()); //doesn't work
         
        if(itemToDrop != null)
        {
            Debug.Log("called item isn't null");
            player.DropItem(itemToDrop); //drops item
            player.Inventory.Remove(SlotID); //removes item
            Refresh(); //refreshes inventory
        }
        
    }


    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);    
        draggedIcon.transform.SetParent(canvas.transform); //makes the icon a child of the canvas so that it shows up properly
        draggedIcon.raycastTarget = false; //so that it doesn't interfere with drag and drop
        draggedIcon.rectTransform.sizeDelta = new Vector2(50,50);
       //need to scale down the icon size to .1 here REMEMBER THIS!!!!!!

        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Start Drag: " + draggedSlot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Dragging: " + draggedSlot.name);
    }

    public void SlotEndDrag()
    {
        Debug.Log("Done Dragging: " + draggedSlot.name);
    }

    public void SlotDrop(Slot_UI slot)
    {
        Debug.Log("Dropping:" + draggedSlot.name + " on " + slot.name); //drags draggedSlot onto Slot
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

  
}


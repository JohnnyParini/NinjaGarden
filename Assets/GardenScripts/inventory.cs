using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class inventory 
{
    
    [System.Serializable]
   public class Slot
    {
        public string itemName;
        public int numInSlot;
        public int maxAllowed;

        public int check;

        public Sprite icon;

        

        public Slot()
        {
            itemName = "";
            numInSlot = 0; //num in slot not working correctly (only when removing. removes an item from inventory 0 instead of toolbar)
            maxAllowed = 99; 
        }

        public bool isEmpty()
        {
                //get?
                if(itemName == "" && numInSlot == 0)
                {
                Debug.Log("isEmpty is true");
                    return true;
                }
            Debug.Log("isEmpty is false");
                return false;
            
        }

        public bool canAddItem(string itemName)
        {

            if(this.itemName == itemName && numInSlot < maxAllowed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addItem(Item item)
        {
            Debug.Log("This the the entered type " + itemName);
            Debug.Log("This is the entered item " + item);
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;

            numInSlot++;
            check++;
            //Debug.Log("something has entered the inventory");
        }

        public void addItem(string itemName, Sprite icon, int maxAllowed)
        {
            this.itemName = itemName;
            this.icon = icon;
            numInSlot++;
            this.maxAllowed = maxAllowed;
        }

        public void removeItem()
        {
            Debug.Log("inventory removeItem has been called"); //THIS IS REALLY HUGE! I FOUND OUT THAT THIS REMOVEITEM FUNCTION ISN'T CALLED FOR THE MELON OR STRAWBERRY 
            
            if (numInSlot > 0)//at least 1 item to remove
            {
                
                numInSlot--; //remove an item
                check--;
                Debug.Log("the numInSlot has gone down");
                Debug.Log("the numInSlot is currently " + numInSlot);
                //check = numInSlot;
                Debug.Log("check = " + check);


                if (numInSlot == 0) //if the number goes to 0
                {
                    this.icon = null;
                    this.itemName = "";
                    Debug.Log("I hath set the icon to null");
                }
            }
        }

    }

    public List<Slot> slots = new List<Slot>();

    public Slot selectedSlot;

    public inventory(int numSlots) //constructor
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
           
        }
    }


    public void Add(Item item)
    {
        Debug.Log("This is the type to add " + item.data.itemName);
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName && slot.canAddItem(item.data.itemName)) //|| slot.itemName == "" potentially needed
            {

                slot.addItem(item);
                Debug.Log("added " + item.name + " to slots");
                return;

            }
        }

        foreach(Slot slot in slots)
            {
                if(slot.itemName == "")
                {
                    slot.addItem(item);
                    return;
                }
            }
                //maybe this below code isn't needed

               // if(slot.itemName == CollectableType.NONE){
                 //   slot.itemName = item.type;
                   // Debug.Log("changing type to "+ item.type);
                    //slot.icon = item.icon;
                    //Debug.Log("changing icon to " + item.icon); //THIS IS HUGE. I FOUND OUT THAT WHEN YOU PICK UP A DROPPED ITEM, THE TYPE IS NULL
                    //if(slot.itemName != item.type)
                    //{
                     //   Debug.Log("Something has gone wrong");
                   // }
                    //Debug.Log("slot previous type = " + slot.type);
                //}
                //slot.addItem(item);
                //Debug.Log("Slot final type = " + slot.itemName);
                //return;
            //}   
    }

    public void Remove(int index)
    {
        Debug.Log("Calling Remove");
        slots[index].removeItem();
        Debug.Log("removing an item from " + index);
    }
    public void Remove(int index, int NumToRemove)
    {
        if(slots[index].numInSlot >= NumToRemove)
        {
            for(int i =0; i< NumToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, inventory toInventory, int numToMove =1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (toSlot.isEmpty()) { 
        //if (toSlot.isEmpty() && toSlot.canAddItem(fromSlot.itemName)) 
       //{
            for (int i = 0; i < numToMove; i++)
            {
                toSlot.addItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed);
                fromSlot.removeItem(); //takes the item away from the old slot, and gives it to the new slot 
            }
        }

    }

    public void SelectSlot(int index)
    {
        if(slots != null && slots.Count >0) //if slots exist 
        {
            
            selectedSlot = slots[index];
            Debug.Log("selectedSlot is equal to " + selectedSlot.itemName);
        }
    }

}

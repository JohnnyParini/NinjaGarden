using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class inventory 
{
    
    [System.Serializable]
   public class Slot
    {
        public CollectableType type;
        public int numInSlot;
        public int maxAllowed;

        public int check;

        public Sprite icon;

        

        public Slot()
        {
            type = CollectableType.NONE;
            numInSlot = 0;
            maxAllowed = 99; 
        }

        public bool canAddItem()
        {
            if(numInSlot < maxAllowed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addItem(collectable item)
        {
            Debug.Log("This the the entered type " + type);
            Debug.Log("This is the entered item " + item);
            this.type = item.type;
            this.icon = item.icon;

            numInSlot++;
            check++;
            //Debug.Log("something has entered the inventory");
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
                    this.type = CollectableType.NONE;
                    Debug.Log("I hath set the icon to null");
                }
            }
        }

    }

    public List<Slot> slots = new List<Slot>();

    public inventory(int numSlots) //constructor
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }


    public void Add(collectable item)
    {
        Debug.Log("This is the type to add " + item.type);
        foreach(Slot slot in slots)
        {
            if(slot.type == item.type && slot.canAddItem() || slot.type == CollectableType.NONE)
            {
                if(slot.type == CollectableType.NONE){
                    slot.type = item.type;
                    Debug.Log("changing type to "+ item.type);
                    slot.icon = item.icon;
                    Debug.Log("changing icon to " + item.icon); //THIS IS HUGE. I FOUND OUT THAT WHEN YOU PICK UP A DROPPED ITEM, THE TYPE IS NULL
                    if(slot.type != item.type)
                    {
                        Debug.Log("Something has gone wrong");
                    }
                    //Debug.Log("slot previous type = " + slot.type);
                }
                slot.addItem(item);
                Debug.Log("Slot final type = " + slot.type);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].removeItem();
        Debug.Log("removing an item from " + index);
    }


}

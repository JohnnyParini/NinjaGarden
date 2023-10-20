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
            if (slot.itemName == item.data.itemName && slot.canAddItem()) //|| slot.itemName == "" potentially needed
            {

                slot.addItem(item);
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


}

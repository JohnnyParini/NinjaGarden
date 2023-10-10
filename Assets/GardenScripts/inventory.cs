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
        public int count;
        public int maxAllowed;

        public Sprite icon;

        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            maxAllowed = 99; 
        }

        public bool canAddItem()
        {
            if(count < maxAllowed)
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
            //Debug.Log("This the the entered type " + type);
            this.type = item.type;
            this.icon = item.icon;

            count++;
        }

        public void removeItem()
        {
            if (count > 0) //at least 1 item to remove
            {
                count--; //remove an item
                Debug.Log("the count has gone down");
                Debug.Log("the count is currently " + count);

                if(count == 0)
                {
                    icon = null;
                    type = CollectableType.NONE;
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
        //Debug.Log("This is the type to add " + typeToAdd);
        foreach(Slot slot in slots)
        {
            if(slot.type == item.type && slot.canAddItem() || slot.type == CollectableType.NONE)
            {
                if(slot.type == CollectableType.NONE){
                    slot.type = item.type;
                    //Debug.Log("changing type");
                    if(slot.type != item.type)
                    {
                        Debug.Log("Something has gone wrong");
                    }
                    //Debug.Log("slot previous type = " + slot.type);
                }
                slot.addItem(item);
                //Debug.Log("Slot final type = " + slot.type);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].removeItem();
    }


}

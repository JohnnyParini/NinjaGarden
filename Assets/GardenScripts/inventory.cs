using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory 
{
   public class Slot
    {
        public CollectableType type;
        public int count;
        public int maxAllowed;

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

        public void addItem(CollectableType type)
        {
            this.type = type;
            count++;
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


    public void Add(CollectableType typeToAdd)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == typeToAdd && slot.canAddItem())
            {
                slot.addItem(typeToAdd);
                return;
            }
        }
    }

}

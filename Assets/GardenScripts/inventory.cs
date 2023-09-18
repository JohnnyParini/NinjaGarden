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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GardenItemManager : MonoBehaviour
{
    
    public Item[] items; //MAKE SURE THAT THE ITEM YOU WANT TO PICK UP IS IN THE COLLECTABLE ITEMS TAB 

   

    //NEW: make sure that the prefab takes the "itemData" in the inspector. this is not hard

    private Dictionary<string, Item> nameToItemDict = new Dictionary<string, Item>(); //allows for the assosiation of Keys and Values
    //matchs a string and an item

    private void Awake()
    {
        foreach(Item item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Item item)
    {
        Debug.Log("the added item name is " + item.name);
        if (!nameToItemDict.ContainsKey(item.data.itemName)) //if the dictionary doesn't have the key type
        {
            nameToItemDict.Add(item.data.itemName, item); //add to the dictionary
        }
    }

    public Item GetItemByName(string key)
    {
        Debug.Log("The called item type is " + key);
        if (nameToItemDict.ContainsKey(key)) //if true, it does contain this specific key
        {
            Debug.Log("the called type is in the dict");
            return nameToItemDict[key]; //return the type
        }
        else
        {
            Debug.Log("the called item is null");
            return null;
        }
    }

}

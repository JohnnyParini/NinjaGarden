using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenItemManager : MonoBehaviour
{
    
    public collectable[] collectableItems; //MAKE SURE THAT THE ITEM YOU WANT TO PICK UP IS IN THE COLLECTABLE ITEMS TAB

    private Dictionary<CollectableType, collectable> collectableItemsDict = new Dictionary<CollectableType, collectable>(); //allows for the assosiation of Keys and Values

    private void Awake()
    {
        foreach(collectable item in collectableItems)
        {
            AddItem(item);
        }
    }

    private void AddItem(collectable item)
    {
        if (!collectableItemsDict.ContainsKey(item.type)) //if the dictionary doesn't have the key type
        {
            collectableItemsDict.Add(item.type, item); //add to the dictionary
        }
    }

    public collectable GetItemByType(CollectableType type)
    {
        Debug.Log("The called item type is " + type);
        if (collectableItemsDict.ContainsKey(type)) //if true, it does contain this specific key
        {
            Debug.Log("the called type is in the dict");
            return collectableItemsDict[type]; //return the type
        }
        else
        {
            Debug.Log("the called item is null");
            return null;
        }
    }

}

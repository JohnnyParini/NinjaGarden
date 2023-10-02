using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenItemManager : MonoBehaviour
{
    
    public collectable[] collectableItems;

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
            collectableItemsDict.Add(item.type, item);
        }
    }

    public collectable GetItemByType(CollectableType type)
    {
        if (collectableItemsDict.ContainsKey(type)) //if true, it does contain this specific key
        {
            return collectableItemsDict[type];
        }
        else
        {
            return null;
        }
    }

}

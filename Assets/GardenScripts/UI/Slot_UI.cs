using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    public int SlotID;
    public inventory Inventory;

    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    [SerializeField] private GameObject highlight;
    public void SetItem(inventory.Slot slot)
    {
        if(slot != null)
        {
            itemIcon.sprite = slot.icon;
            //itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.numInSlot.ToString();
            Debug.Log("the Slot_UI count variable is " + slot.numInSlot);
        }
    }

    public void SetEmpty()
    {
        //Debug.Log("SETTING EMPTY EMPTY EMPTY EMPTY EMPTY");
        itemIcon.sprite = null; //display white square
        //itemIcon.color = new Color(1, 1, 1, 1); //invisible image
        //Debug.Log("just set the color to white");
        quantityText.text = "0"; //don't want to display text, have nothing in the slot
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public void SetItem(inventory.Slot slot)
    {
        if(slot != null)
        {
            itemIcon.sprite = slot.icon;
            //itemIcon.color = new Color(1, 1, 1, 250);
            quantityText.text = slot.count.ToString();
        }
    }

    public void SetEmpty()
    {
        Debug.Log("SETTING EMPTY EMPTY EMPTY EMPTY EMPTY");
        itemIcon.sprite = null; //display white square
        itemIcon.color = new Color(1, 1, 1, 1); //invisible image
        Debug.Log("just set the color to white");
        quantityText.text = "0"; //don't want to display text, have nothing in the slot
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextExample : MonoBehaviour
{
    //public static ScrollingTextExample textInstance;
    [Header("Text Settings")]
    [SerializeField] [TextArea] private string[] itemInfo;
    [SerializeField] private float textSpeed = 0.01f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI itemInfoText;
    public int currentlyDisplayingText = 0; //find ways to adjust this to get differnt dialogue to play

    private void Start()
    {
        currentlyDisplayingText = 0;
    }
    private void Update()
    {
        //make this automatic instead of depending on the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateText();
        }
    }
    public void upTextNum()
    {
        currentlyDisplayingText++;
    }
    public void downTextNum()
    {
        currentlyDisplayingText--;
    }


    public void ActivateText()
    {
        //add some if statement to avoid out of bounds
       
        StartCoroutine(AnimateText()); 
    }

    IEnumerator AnimateText()

    {
        for (int i = 0; i < itemInfo[currentlyDisplayingText].Length +1; i++)
        {
            itemInfoText.text = itemInfo[currentlyDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }

    }

}

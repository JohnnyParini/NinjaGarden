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
    public bool transition = false;
    public bool textActive = false;

    private void Start()
    {
        currentlyDisplayingText = 0;
    }
    private void Update()
    {
        //make this automatic instead of depending on the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transition = false;
            ActivateText();
        }
        
    }
    public void upTextNum()
    {
    
        currentlyDisplayingText++;
        setTextBlank();
        stopTextScroll();
        //transition = false;
        //ActivateText();
        //stop on transition
    }
    public void downTextNum()
    {
        currentlyDisplayingText--;
        setTextBlank();
        stopTextScroll();
        //transition = false;
        //ActivateText();
    }

    public void setTextBlank()
    {
        itemInfoText.text = "";
        textActive = false;
    }

    public void stopTextScroll() {
        transition = true;
    }


    public void ActivateText()
    {
        //add some if statement to avoid out of bounds
        if (!textActive)
        {
            StartCoroutine(AnimateText()); //make this stop on transition
        }
    }

    IEnumerator AnimateText()

    {
        textActive = true;
        for (int i = 0; i < itemInfo[currentlyDisplayingText].Length +1; i++)
        {
            //yield return new WaitForSeconds(.05f);
            itemInfoText.text = itemInfo[currentlyDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
            if (transition)
            {
                yield break;
            }
        }

    }

}

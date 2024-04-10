using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject[] background;
    [SerializeField] GameObject exampleText;
    int index;


    private void Start()
    {
        currentlyDisplayingText = 0;
        itemInfoText.text = "";
        index = 0;
        ActivateText();
    }
    private void Update()
    {
        //make this automatic instead of depending on the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transition = false;
            ActivateText();
        }
        /*if(currentlyDisplayingText == 0)
        {
            ActivateText();
        }*/

        if (index < 0)
        {
            index = background.Length; //loop
        }

        if (index > background.Length)
        {
            index = 0; //loop
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            escapeLevel();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            backImage();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextImage();

            //Debug.Log("right");
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
        itemInfoText.text = "";
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

    public void nextImage()
    {
        if (index < background.Length - 1)
        {
            index += 1;
            upTextNum();
            backgroundLoad();
            ActivateText();

        }
    }

    public void backImage()
    {
        if (index > 0)
        {
            index -= 1;
            downTextNum();
            backgroundLoad();

        }
    }

    public void backgroundLoad()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].SetActive(false);
            background[index].SetActive(true);
            //Debug.Log(index);
        }
    }

    public void escapeLevel()
    {
        SceneManager.LoadScene(0);
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

//THIS IS THE REAL SLIDESHOW SCRIPT USE THIS SCRIPT TO EDIT THE SLIDESHOW CODING

//public ScrollingTextExample textInstance;
// Start is called before the first frame update

// Update is called once per frame





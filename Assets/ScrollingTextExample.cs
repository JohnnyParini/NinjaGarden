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
    public bool finishedLoad = false;
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
            Debug.Log("Starting text animation");
            //ActualAnimateText();
            StartCoroutine(AnimateText()); //make this stop on transition
            Debug.Log("Text has been animated");
        }
    }

    public void nextImage()
    {
        if (index < background.Length - 1)
        {
            index += 1;
            backgroundLoad();
            upTextNum();
            
            //DoDelayAction(1);
            Debug.Log("starting text call");
            ActivateText();
            Debug.Log("text has been called");


        }
    }

    public void backImage()
    {
        if (index > 0)
        {
            index -= 1;
            backgroundLoad();
            downTextNum();
            

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

    public void ActualAnimateText()
    {
        textActive = true;
        for (int i = 0; i < itemInfo[currentlyDisplayingText].Length+1; i++)
        {
            itemInfoText.text = itemInfo[currentlyDisplayingText].Substring(0, i);
            if(i == itemInfo[currentlyDisplayingText].Length)
            {
                Debug.Log("text finished loading");
                //return;
            }
            DoDelayAction(textSpeed);
            if (transition)
            {
                Debug.Log("transition detected");
            }
        }
    }

    void DoDelayAction(float delayTime)
    {
        
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        Debug.Log("delaying a call");
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
    }

    IEnumerator AnimateText()

    {
        Debug.Log("Beginning of the actual animation");
        textActive = true;
        for (int i = 0; i < itemInfo[currentlyDisplayingText].Length +1; i++)
        {
            //yield return new WaitForSeconds(.05f);
            itemInfoText.text = itemInfo[currentlyDisplayingText].Substring(0, i);
            if(i == itemInfo[currentlyDisplayingText].Length)
            {
                Debug.Log("text finished loading"); //found out that this is being called twice 
                yield break;
            }
            yield return new WaitForSeconds(textSpeed);
            if (transition) //detecting a transition when there isn't one
            {
                Debug.Log("transition detected");
                setTextBlank();
                
                yield break;
             
            }
        }

    }

}

//THIS IS THE REAL SLIDESHOW SCRIPT USE THIS SCRIPT TO EDIT THE SLIDESHOW CODING

//public ScrollingTextExample textInstance;
// Start is called before the first frame update

// Update is called once per frame





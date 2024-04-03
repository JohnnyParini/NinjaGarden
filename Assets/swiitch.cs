using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class swiitch : MonoBehaviour
{
    public GameObject[] background;
    int index;

    //THIS IS THE REAL SLIDESHOW SCRIPT USE THIS SCRIPT TO EDIT THE SLIDESHOW CODING

    public ScrollingTextExample textInstance;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index < 0)
        {
            index = background.Length; //loop
        }

        if(index > background.Length)
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
            
            Debug.Log("right");
        }

    }

    public void nextImage()
    {
        if (index < background.Length-1)
        {
            index += 1;
            textInstance.upTextNum();
            backgroundLoad();
        }
    }

    public void backImage()
    {
        if (index > 0)
        {
            index -= 1;
            textInstance.downTextNum();
            backgroundLoad();
        }
    }

    public void backgroundLoad()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].SetActive(false);
            background[index].SetActive(true);
            Debug.Log(index);
        }
    }

    public void escapeLevel()
    {
        SceneManager.LoadScene(0);
    }

}

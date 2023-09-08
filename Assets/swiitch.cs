using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class swiitch : MonoBehaviour
{
    public GameObject[] background;
    int index;



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



    }

    public void nextImage()
    {
        if (index < background.Length-1)
        {
            index += 1;
            backgroundLoad();
        }
    }

    public void backImage()
    {
        if (index > 0)
        {
            index -= 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    public static Persistence instance;


     private void Awake()
    {

        //if level.buildIndex == 0 {
        //disable this;
        //}
        //DontDestroyOnLoad(gameObject);
       
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        { //if it is in the intro menu (can mod this so that it is only active in garden scenes)
            gameObject.SetActive(false);
            Debug.Log(gameObject.name + " is being set deActive");
        }
        else if(SceneManager.GetActiveScene().buildIndex ==1)
        {
            gameObject.SetActive(true);
            Debug.Log(gameObject.name + " is being set Active");
        } 
    }
}

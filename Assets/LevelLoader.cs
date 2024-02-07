using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    [SerializeField] public int LevelToLoad;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetMouseButtonDown(0))
        //{
         //   Debug.Log("Loading level");
          //  LoadNextLevel();
        //}      
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //reset player so that it doesn't keep colliding
        LoadLevelByNumber(LevelToLoad);

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevelByNumber(int numToLoad)
    {
        Debug.Log("Triggereing animation");
        StartCoroutine(LoadLevel(numToLoad));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        Debug.Log("animation triggered");

        //wait for animation to stop playing
        yield return new WaitForSeconds(transitionTime);



        //load scene
        SceneManager.LoadScene(LevelIndex);
    }



}

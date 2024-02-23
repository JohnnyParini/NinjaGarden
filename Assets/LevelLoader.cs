using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    [SerializeField] public int LevelToLoad;

    public float transitionTime = 1f;

    private Vector3 startPosition = new Vector3(0f,0f,0f);

    public Player player;

    private void Awake()
    {
        
    }

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
        player.transform.position = startPosition;
        Debug.Log("I have reset the player position");
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

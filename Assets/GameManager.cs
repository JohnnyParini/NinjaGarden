using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //single design sytem so that it isn't called multiple times
    public static GameManager instance;

    public GardenItemManager itemManager;

    public TileManager tileManager;

    public Player player;

    public UI_Manager uiManager;

    private void Awake()
    {
        if(instance != null && instance != this) //if an instance already exists
        {
            Destroy(this.gameObject); //to make sure that only one GameManager exists
        }
        else //if an instance doesn't already exists.
        {
            instance = this;
        }

        DontDestroyOnLoad(this); //makes sure that it stays between scenes 

        itemManager = GetComponent<GardenItemManager>();
        tileManager = GetComponent<TileManager>();
        uiManager = GetComponent<UI_Manager>();

        player = FindObjectOfType<Player>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void loadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }




}

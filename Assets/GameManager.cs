using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
    //single design sytem so that it isn't called multiple times
    public static GameManager instance;

    public GardenItemManager itemManager;

    public TileManager tileManager;

    public Player player;

    public UI_Manager uiManager;

    public SceneSwapManager sceneSwapManager;

    public LevelDataStorage levelDataStorage;

   

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            Debug.Log("Actually Loaded level 1");
            setLevelAwake();
        }
    }
    private void setLevelAwake()
    {
        player.setAwake();
        tileManager.setAwake();
        uiManager.setAwake();
        
        itemManager.setAwake();
    }

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
        levelDataStorage = GetComponent<LevelDataStorage>();
        

        player = FindObjectOfType<Player>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //tileManager.setAwake();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //note for level unlock storage: use dictionaries with int keys to represent level number and a tuple definition in order to store relevent data characteristics for level
    //tuple stores lvl beaten bool, boss lvl status, cur high score 


    public void loadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }

   


}

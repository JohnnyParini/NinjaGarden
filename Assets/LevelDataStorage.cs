using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataStorage : MonoBehaviour
{
    // Start is called before the first frame update
    //bool = level completions status
    //int = highscore 
    //int = active score
    //addd aditional definition to dictionary that defines which scenes belong to which level
    //may need to create inverse dictionary that defines the level for each scene i.e scene 1 = lvl, scene 2 = lvl 1, scene 3 = lvl 2 etc.
    
    public Dictionary<int, (bool, int, int)> lvls = new Dictionary<int, (bool, int, int)>();
    public int lvlCount;
    public int curLvl;
    public int curLvlOld;

    void Start()
    {
        for (int i = 0; i < lvlCount; i++)
        {
            lvls.Add(i,(false, 0, 0));
        }
        //curLvl = SceneManager.GetActiveScene().buildIndex;
        //curLvlOld = curLvl;
    }

    // Update is called once per frame
    void Update()
    {
        //update method not most efficient place to check if scene has changed, use on scene was loaded instead
        /*
        curLvl = SceneManager.GetActiveScene().buildIndex;
        if (curLvl != curLvlOld)
        {

        }
        */
    }

   
}

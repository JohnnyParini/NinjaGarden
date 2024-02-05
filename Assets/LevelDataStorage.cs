using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataStorage : MonoBehaviour
{
    // Start is called before the first frame update
    //bool = level completions status
    //int = highscore 
    
    Dictionary<int, (bool, int)> lvls = new Dictionary<int, (bool, int)>();
    public int lvlCount;

    void Start()
    {
        for (int i = 0; i < lvlCount; i++)
        {
            lvls.Add(i,(false, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

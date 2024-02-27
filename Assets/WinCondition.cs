using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class WinCondition
{
    // Start is called before the first frame update
    //public GameObject boss;
    //public GameObject gameManager;
    //public GameObject lvl;
    //public LevelDataStorage lvlData;
    //public int curLvl;
    //public int hs;
    //public bool completed;
  

    public static void Win() //hs = highscore, the score after beating the level
    {
        int hs;
        bool completed = true;
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        int curLvl = gm.getActiveLevel.curLvl;
        LevelDataStorage lvlData = gm.GetComponent<GameManager>().levelDataStorage;
        GameObject completeLvlUI = gm.getActiveLevel.winScreen;

        completeLvlUI.SetActive(true);
        

        if (lvlData.lvls[curLvl].Item3 > lvlData.lvls[curLvl].Item2)
        {
            hs = lvlData.lvls[curLvl].Item3; //new highest score recorded
        }
        else
        {
            hs = lvlData.lvls[curLvl].Item2; //old high score is still the high score
        }
        lvlData.lvls[curLvl] = new(completed, hs, 0);
        Debug.Log(lvlData.lvls[curLvl] + " VICTORY DATA");
        completed = false;
    }
}

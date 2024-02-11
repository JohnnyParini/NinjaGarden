using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss;
    public GameObject gameManager;
    public GameObject lvl;
    public LevelDataStorage lvlData;
    public int curLvl;
    public int hs;
    public bool completed;
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        lvl = GameObject.FindGameObjectWithTag("Level");
        curLvl = lvl.GetComponent<CurrentLevel>().thisLvl;
        Debug.Log(curLvl);
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        lvlData = gameManager.GetComponent<GameManager>().levelDataStorage;
        Debug.Log(lvlData.lvls[curLvl] + " INITIAL DATA");
    }

    public void Win() //hs = highscore, the score after beating the level
    {
        completed = true;
        if (lvlData.lvls[curLvl].Item3 > lvlData.lvls[curLvl].Item2) //
        {
            hs = lvlData.lvls[curLvl].Item3; //new highest score recorded
        }
        else
        {
            hs = lvlData.lvls[curLvl].Item2; //old high score is still the high score
        }
        lvlData.lvls[curLvl] = new(completed, hs, 0);
        Debug.Log(lvlData.lvls[curLvl] + " VICTORY DATA");
    }
}

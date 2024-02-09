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
    public bool victory;
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

    public void Win()
    {
        victory = true;
        if (hs > lvlData.lvls[curLvl].Item2)
        {
            hs = hs;
        }
        else
        {
            hs = lvlData.lvls[curLvl].Item2;
        }
        lvlData.lvls[curLvl] = new(victory, hs, 0);
        Debug.Log(lvlData.lvls[curLvl] + " VICTORY DATA");
    }
}

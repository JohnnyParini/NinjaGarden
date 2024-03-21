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
        int curLvl = GetActiveLevel.curLvl;
        LevelDataStorage lvlData = gm.GetComponent<GameManager>().levelDataStorage;
        GameObject winScreen = gm.getActiveLevel.winScreen;
        winScreen.SetActive(true);

        TextMeshProUGUI st = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI hst = GameObject.Find("High Score Text").GetComponent<TextMeshProUGUI>();

        st.text = "Score: " + lvlData.lvls[curLvl].Item3;
        //winScreen.GetComponentInChildren<TextMeshProUGUI>().text

        if (lvlData.lvls[curLvl].Item3 > lvlData.lvls[curLvl].Item2)
        {
            hs = lvlData.lvls[curLvl].Item3; //new highest score recorded
        }
        else
        {
            hs = lvlData.lvls[curLvl].Item2; //old high score is still the high score
        }
        hst.text = "High Score: " + hs;
        lvlData.lvls[curLvl] = new(completed, hs, 0);
        Debug.Log(lvlData.lvls[curLvl] + " VICTORY DATA");

        completed = false;
    }
}

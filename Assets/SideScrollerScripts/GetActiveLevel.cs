using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetActiveLevel : MonoBehaviour
{

    public string sceneName;
    public string startTokenL = "[";
    public string endTokenL = "]";
    public string startTokenS = "(";
    public string endTokenS = ")";
    public string strLvl;
    public int curLvl;
    public string strScene;
    public int curScene;
    public GameObject ninjaPlayer;
    public SidePlayerMasterScript ninjaLogic;
    public GameObject winCondition;
    public WinCondition winConditionLogic;

    void Start()
    {
        SceneManager.activeSceneChanged += GetLevel;
        ninjaPlayer = GameObject.FindGameObjectWithTag("Player");
        ninjaLogic = ninjaPlayer.GetComponent<SidePlayerMasterScript>();
    }
    public void GetLevel(Scene current, Scene next)
    {
        winCondition = GameObject.FindGameObjectWithTag("WinCondition");
        winConditionLogic = winCondition.GetComponent<WinCondition>();
        sceneName = next.name;
        strLvl = StringParsing.getBetween(sceneName, startTokenL, endTokenL);
        strScene = StringParsing.getBetween(sceneName, startTokenS, endTokenS);
        Debug.Log(strLvl + " The Level");
        if (strLvl != "")
        {
            curLvl = int.Parse(strLvl);
            curScene = int.Parse(strScene);
            Debug.Log(curLvl + " Int form of level");
            Debug.Log(curScene + " Int form of scene");
            ninjaLogic.curLvl = curLvl;
            winConditionLogic.curLvl = curLvl;
        }
        else
        {
            curLvl = 0;
        }
        ninjaPlayer.transform.position = new Vector3(-4f, 2f, 99.96342f);

        if (curScene == 1)
        {
            ninjaLogic.currentHealth = ninjaLogic.maxHealth;
        }
        else
        {
            //ninjaLogic.healthText = 
        }
    }
}

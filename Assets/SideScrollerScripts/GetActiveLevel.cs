using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GetActiveLevel : MonoBehaviour
{

    public string sceneName;
    public string startTokenL = "[";
    public string endTokenL = "]";
    public string startTokenS = "(";
    public string endTokenS = ")";
    public string strLvl;
    public static int curLvl;
    public string strScene;
    public int curScene;
    public GameObject ninjaPlayer;
    public SidePlayerMasterScript ninjaLogic;
    public GameObject winCondition;
    public GameObject winScreen;
    //public WinCondition winConditionLogic;

    void Start()
    {
        SceneManager.activeSceneChanged += GetLevel;
        ninjaPlayer = GameObject.FindGameObjectWithTag("Player");
        ninjaLogic = ninjaPlayer.GetComponent<SidePlayerMasterScript>();
    }
    public void GetLevel(Scene current, Scene next)
    {
        ninjaLogic.healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        //winConditionLogic = winCondition.GetComponent<WinCondition>();
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
            //winConditionLogic.curLvl = curLvl;
        }
        else
        {
            curLvl = 1; //default level should be 0 for garden scene
        }
        ninjaPlayer.transform.position = new Vector3(-4f, 2f, 99.96342f); //transform player to same position in every new scene for consistency

       
        if (curScene == 1) //if the scene is the start of the level, reset player health to max
        {
            ninjaLogic.currentHealth = ninjaLogic.maxHealth;
            Debug.Log("HEALTH SET TO MAX");
        }

        ninjaLogic.healthText.text = "Health: " + ninjaLogic.currentHealth;

        winScreen = GameObject.FindGameObjectWithTag("CompletionScreen");
        //winScreen = 
        winScreen.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetActiveLevel : MonoBehaviour
{

    public string sceneName;
    public string startToken = "[";
    public string endToken = "]";
    public string strLvl;
    public int curLvl;
    public GameObject ninjaPlayer;
    public SidePlayerMasterScript ninjaLogic;
    public GameObject winCondition;
    public WinCondition winConditionLogic;

    //SceneManager.activeSceneChanged += GetLevel;


    // Start is called before the first frame update
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
        strLvl = StringParsing.getBetween(sceneName, startToken, endToken);
        Debug.Log(strLvl + " The Level");
        if (strLvl != "")
        {
            curLvl = int.Parse(strLvl);
            Debug.Log(curLvl + "Int form of level");
            ninjaLogic.curLvl = curLvl;
            winConditionLogic.curLvl = curLvl;
        }
    }
}

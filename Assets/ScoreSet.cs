using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSet : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public LevelDataStorage lvlData;
    public GameObject lvl;
    public int curLvl;

    void Start()
    {
        lvlData = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().levelDataStorage;
        scoreText = this.GetComponent<TextMeshProUGUI>();
        curLvl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getActiveLevel.curLvl;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + lvlData.lvls[curLvl].Item3;
    }
}

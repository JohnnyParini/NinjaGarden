using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSet : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public LevelDataStorage lvlData;

    void Start()
    {
        lvlData = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().levelDataStorage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

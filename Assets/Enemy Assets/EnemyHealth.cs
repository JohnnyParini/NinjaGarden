using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    int currentHealth;
    public bool isBoss;
    public bool win;
    public LevelDataStorage lvlData;
    public GameObject gameManager;
    public int score;
    public int curScore;
    public int curLvl;
    public GameObject lvl;
    public WinCondition wc;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        currentHealth = maxHealth;
        curLvl = GameObject.FindGameObjectWithTag("Level").GetComponent<CurrentLevel>().thisLvl;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        lvlData = gameManager.GetComponent<GameManager>().levelDataStorage;
        //scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
    }

  

    public void takeDamage(int damage)
    {
        //Debug.Log(damage);
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            death();
        }
    }

    void death()
    {
        Debug.Log("He be ded");
        GetComponent<Collider2D>().enabled = false;
        Object.Destroy(gameObject);
        curScore = lvlData.lvls[curLvl].Item3;
        lvlData.lvls[curLvl] = new (lvlData.lvls[curLvl].Item1, lvlData.lvls[curLvl].Item2, curScore += score);
        Debug.Log(lvlData.lvls[curLvl] + " ALL THE DATA HERE");
        //scoreText.text = "Score: " + lvlData.lvls[curLvl].Item3;

        if (this.CompareTag("Boss"))
        {
            wc = GameObject.FindGameObjectWithTag("WinCondition").GetComponent<WinCondition>();
            wc.Win();
        }

    }

   
}

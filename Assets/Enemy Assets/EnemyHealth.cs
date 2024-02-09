using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        lvl = GameObject.FindGameObjectWithTag("Level");
        wc = GameObject.FindGameObjectWithTag("WinCondition").GetComponent<WinCondition>();
        curLvl = lvl.GetComponent<CurrentLevel>().thisLvl;
        currentHealth = maxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        lvlData = gameManager.GetComponent<GameManager>().levelDataStorage;

    }

  

    public void takeDamage(int damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            death();
        }
    }

    void death()
    {
        Debug.Log("He be ded");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        curScore = lvlData.lvls[curLvl].Item3;
        lvlData.lvls[curLvl] = new (lvlData.lvls[curLvl].Item1, lvlData.lvls[curLvl].Item2, curScore += score);

        if (this.CompareTag("Boss"))
        {
            wc.Win();
        }

    }

   
}

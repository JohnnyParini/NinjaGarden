using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(maxHealth);
    }

  

    public void takeDamage(int damage)
    {
        Debug.Log(damage);
        Debug.Log(currentHealth);
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
        
    }
}

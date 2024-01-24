using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTransition : MonoBehaviour
{
    [SerializeField] public int numToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Portal Collision");
        SceneManager.LoadScene(numToLoad);

    }

}

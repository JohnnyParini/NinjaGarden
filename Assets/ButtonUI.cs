using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int lvl;
    public void NewGameButton()
    {
        SceneManager.LoadScene(lvl);
    }
}

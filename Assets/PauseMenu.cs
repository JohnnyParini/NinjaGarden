using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenu;
    public GameObject gameManager;
    LevelDataStorage lvlData;
    public static bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
        lvlData = gameManager.GetComponent<GameManager>().levelDataStorage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToGarden()
    {
        Time.timeScale = 1f;
        lvlData.lvls[GetActiveLevel.curLvl] = new(lvlData.lvls[GetActiveLevel.curLvl].Item1, lvlData.lvls[GetActiveLevel.curLvl].Item2, 0);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        lvlData.lvls[GetActiveLevel.curLvl] = new(lvlData.lvls[GetActiveLevel.curLvl].Item1, lvlData.lvls[GetActiveLevel.curLvl].Item2, 0);
        Application.Quit();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_Menu : MonoBehaviour
{
    public static bool gameIsPaused=false;
    public GameObject pauseMenuUI;
    private string mainMenu = "Main_Menu";


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void loadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu);
    }

    public void quitGame()
    {
        Debug.Log("quitig");
        Application.Quit();
    }
}

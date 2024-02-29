using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu_Manager : MonoBehaviour
{
    //private string gameSceneName = "Main_Scene";
    private level_Loader levelLoader;
    private loading_Scene loadingScene;

    
    private void Start()
    {
        levelLoader = FindObjectOfType<level_Loader>();
        loadingScene = FindObjectOfType<loading_Scene>();
    }
    

    public void PlayGame()
    {
        loadingScene.loadScene(1);
        //SceneManager.LoadScene(gameSceneName);
        //levelLoader.loadNextLevel();
    }


}

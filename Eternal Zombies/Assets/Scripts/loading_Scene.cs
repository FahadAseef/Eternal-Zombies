using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading_Scene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBarFill;

    public void loadScene(int sceneId)
    {
        StartCoroutine(loadSceneAsync(sceneId));
    }

    IEnumerator loadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressvalue= Mathf.Clamp01(operation.progress/0.9f);
            loadingBarFill.fillAmount=progressvalue;
            yield return new WaitForSeconds(progressvalue);
            yield return null;
        }
    }
    
}

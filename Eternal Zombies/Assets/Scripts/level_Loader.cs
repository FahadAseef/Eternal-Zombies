using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_Loader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;

    public void loadNextLevel()
    {
        StartCoroutine(loadLevel());
    }

    IEnumerator loadLevel()
    {
        //play animation
        transition.SetTrigger("start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene("Main_Scene");

    }
  
}

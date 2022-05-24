using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void StartNewGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelIndex);
    }


    public void ReplayGame()
    {
        StartCoroutine(LoadLevel(1));
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}

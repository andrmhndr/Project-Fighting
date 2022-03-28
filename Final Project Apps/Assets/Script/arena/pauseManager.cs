using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;
    public bool gameFinish = false;

    private void OnPause()
    {
        if (!gameFinish)
        {
            if (isPaused)
            {
                resume();

            }
            else
            {
                pause();
            }
        }
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void OnCancel()
    {
        resume();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class paused : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    public void returnMenu()
    {
        GameObject[] pauseManager = GameObject.FindGameObjectsWithTag("pause");
        pauseManager[0].GetComponent<pauseManager>().resume();
        pauseManager[1].GetComponent<pauseManager>().resume();
        GameObject player = GameObject.Find("playerConfigurationManager");
        Destroy(player);
        SceneManager.LoadScene("Main Menu");
    }

    public void resume()
    {
        GameObject[] pauseManager = GameObject.FindGameObjectsWithTag("pause");
        pauseManager[0].GetComponent<pauseManager>().resume();
        pauseManager[1].GetComponent<pauseManager>().resume();
    }
}

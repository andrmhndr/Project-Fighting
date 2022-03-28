using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class matchFinish : MonoBehaviour
{
    public void returnMenu()
    {
        GameObject player = GameObject.Find("playerConfigurationManager");
        Destroy(player);
        SceneManager.LoadScene("Main Menu");
    }

    public void selectCharacter()
    {
        GameObject player = GameObject.Find("playerConfigurationManager");
        Destroy(player);
        SceneManager.LoadScene("Select Character");
    }
}

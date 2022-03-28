using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Button characterButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void setCharacter(GameObject character) 
    { 
        if(!inputEnabled) { return; }

        playerConfigurationManager.instance.setPlayerCharacter(playerIndex, character);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void cancelReady() 
    {
        playerConfigurationManager.instance.setPlayerCharacter(playerIndex, null);
        readyPanel.SetActive(false);
        menuPanel.SetActive(true);
        characterButton.Select();
    }

    public void readyPlayer()
    {
        if (!inputEnabled) { return; }

        playerConfigurationManager.instance.readyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }

    public void back()
    {
        Destroy(GameObject.Find("playerConfigurationManager"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

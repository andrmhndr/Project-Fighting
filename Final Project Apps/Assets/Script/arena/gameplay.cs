using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class gameplay : MonoBehaviour
{
    GameObject[] playerCharacter;

    [SerializeField]
    private TextMeshProUGUI textDisplay;

    public int startLeft = 3;
    public int secondsLeft = 9999;
    public bool takingAway = false;

    public bool gameStart = false;
    public bool gameFinish = false;

    public GameObject finishGame;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI startText;

    public bool flip = false;

    void Start()
    {
        playerCharacter = GameObject.FindGameObjectsWithTag("character");
        textDisplay.SetText("" + secondsLeft);
        startText.SetText("" + (startLeft-1));
    }

    void Update()
    {
        if(startLeft == 0)
        {
            gameStart = true;
            gameBegin();
        }

        if(takingAway == false && startLeft > 0)
        {
            StartCoroutine(timerStart());
        }

        if (takingAway == false && secondsLeft > 0 && !gameFinish && gameStart)
        {
            Destroy(startText);
            StartCoroutine(timerTake());
        }

        if (playerCharacter[0].transform.position.x > playerCharacter[1].transform.position.x && !flip)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            playerCharacter[0].transform.localScale = scale;
            Debug.Log("player1 flip");
            scale.x = 1;
            playerCharacter[1].transform.localScale = scale;
            Debug.Log("player2 flip");
            flip = true;
        } else if(playerCharacter[0].transform.position.x < playerCharacter[1].transform.position.x && flip)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            playerCharacter[0].transform.localScale = scale;
            Debug.Log("player1 flip");
            scale.x = -1;
            playerCharacter[1].transform.localScale = scale;
            Debug.Log("player2 flip");
            flip = false;
        }

        //player wins
        if(playerCharacter[1].GetComponent<playerCombat>().currentHealth <= 0) //player 1
        {

            finishGame.SetActive(true);
            titleText.SetText("Player 1 Wins !");
            gameFinish = true;
            gameOver();
        }
        else if(playerCharacter[0].GetComponent<playerCombat>().currentHealth <= 0) //player 2
        {
            finishGame.SetActive(true);
            titleText.SetText("Player 2 Wins !");
            gameFinish = true;
            gameOver();
        }
        else if (secondsLeft == 0) //times up
        {
            if(playerCharacter[0].GetComponent<playerCombat>().currentHealth > playerCharacter[1].GetComponent<playerCombat>().currentHealth)
            {
                finishGame.SetActive(true);
                titleText.SetText("Times Up ! Player 1 Wins !");
                gameFinish = true;
                gameOver();
            }
            else if(playerCharacter[0].GetComponent<playerCombat>().currentHealth < playerCharacter[1].GetComponent<playerCombat>().currentHealth)
            {
                finishGame.SetActive(true);
                titleText.SetText("Times Up ! Player 2 Wins !");
                gameFinish = true;
                gameOver();
            }
            else if (playerCharacter[0].GetComponent<playerCombat>().currentHealth == playerCharacter[1].GetComponent<playerCombat>().currentHealth)
            {
                finishGame.SetActive(true);
                titleText.SetText("Times Up ! Draw !");
                gameFinish = true;
                gameOver();
            }
        }
    }

    IEnumerator timerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.SetText(""+secondsLeft);
        takingAway = false;
    }

    IEnumerator timerStart()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        startLeft -= 1;
        if(startLeft == 1)
        {
            startText.SetText("FIGHT!");
        }
        else
        {
            startText.SetText("" + (startLeft-1));
        }
        takingAway = false;
    }

    public void gameBegin()
    {
        GameObject[] character = GameObject.FindGameObjectsWithTag("character");
        character[0].GetComponent<playerControl>().gameStart = gameStart;
        character[1].GetComponent<playerControl>().gameStart = gameStart;
    }

    public void gameOver()
    {
        GameObject[] character = GameObject.FindGameObjectsWithTag("character");
        character[0].GetComponent<playerControl>().gameFinish = gameFinish;
        character[1].GetComponent<playerControl>().gameFinish = gameFinish;
        GameObject[] player = GameObject.FindGameObjectsWithTag("pause");
        player[0].GetComponent<pauseManager>().gameFinish = gameFinish;
        player[1].GetComponent<pauseManager>().gameFinish = gameFinish;
    }
}

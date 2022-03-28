using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intializeLevel : MonoBehaviour
{

    [SerializeField]
    private Transform[] playerSpawns;
    private healthBar playerHealthBar;
    string charName;

    [SerializeField]
    private GameObject pauseManager;
    public GameObject pauseMenu;

    GameObject[] playerParent;

    void Awake()
    {
        var playerConfigs = playerConfigurationManager.instance.getPlayerConfigs().ToArray();
        
        playerParent = GameObject.FindGameObjectsWithTag("Player");
        GameObject healthBar1 = GameObject.Find("healthBar1");
        GameObject healthBar2 = GameObject.Find("healthBar2");
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerConfigs[i].playerCharacter, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.transform.parent = playerParent[i].transform;
            var pause = Instantiate(pauseManager);
            pause.GetComponent<pauseManager>().pauseMenu = pauseMenu;
            pause.transform.parent = playerParent[i].transform;
            charName = player.GetComponent<playerCombat>().charName;
            if(i == 1)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                player.transform.localScale = scale;
                playerHealthBar = healthBar2.GetComponent<healthBar>();
                player.GetComponent<playerCombat>().healthBar = playerHealthBar;
                player.GetComponent<playerCombat>().player = 2;
                playerHealthBar.charLogo(charName);
            }
            else
            {
                playerHealthBar = healthBar1.GetComponent<healthBar>();
                player.GetComponent<playerCombat>().healthBar = playerHealthBar;
                player.GetComponent<playerCombat>().player = 1;
                playerHealthBar.charLogo(charName);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class playerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int maxPlayer = 2;

    [SerializeField]
    private GameObject press;

    public static playerConfigurationManager instance { get; set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Singleton");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    private void Update()
    {
        if(playerConfigs.Count == maxPlayer-1)
        {
            Destroy(press);
        }
    }

    public List<PlayerConfiguration> getPlayerConfigs()
    {
        return playerConfigs;
    }

    public void setPlayerCharacter(int index, GameObject character)
    {
        playerConfigs[index].playerCharacter = character;
    }

    public void readyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.Count == maxPlayer && playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("arena");
        }
    }

    public void handlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("player joined " + pi.playerIndex);
        if (!playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }
    public PlayerInput input { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
    public GameObject playerCharacter { get; set; }
}

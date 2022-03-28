using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    void Awake()
    {
        GameObject[] Music = GameObject.FindGameObjectsWithTag("music");

        if (Music.Length == 1)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        } else if(Music.Length == 2)
        {
            Destroy(Music[1]);
        }
    }
}

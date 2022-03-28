using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class optionMenu : MonoBehaviour
{
    public mainMenu mainMenu;
    [SerializeField]
    private Button thisMenuButton;

    public AudioMixer audioMixer;

    public bool fullscreen = true;
    [SerializeField]
    public Image on,off;

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void setFullscreen()
    {
        if(fullscreen)
        {
            fullscreen = false;
            Screen.fullScreen = fullscreen;
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);
        }else if (!fullscreen)
        {
            fullscreen = true;
            Screen.fullScreen = fullscreen;
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
        }
    }

    private void OnCancel()
    {
        mainMenu.gameObject.SetActive(true);
        thisMenuButton.Select();
        this.gameObject.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructionsMenu : MonoBehaviour
{
    public mainMenu mainMenu;
    [SerializeField]
    private Button instructions;

    private void OnCancel()
    {
        mainMenu.gameObject.SetActive(true);
        instructions.Select();
        this.gameObject.SetActive(false);
    }
}

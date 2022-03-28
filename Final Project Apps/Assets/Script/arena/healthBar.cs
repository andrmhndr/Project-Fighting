using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField]
    public GameObject bode;
    [SerializeField]
    public GameObject theo;
    [SerializeField]
    public GameObject miftah;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void charLogo(string charName)
    {
        if(charName == "bode")
        {
            bode.SetActive(true);
            miftah.SetActive(false);
            theo.SetActive(false);
        }else if (charName == "miftah")
        {
            bode.SetActive(false);
            miftah.SetActive(true);
            theo.SetActive(false);
        }else if (charName == "theo")
        {
            bode.SetActive(false);
            miftah.SetActive(false);
            theo.SetActive(true);
        }
    }
}

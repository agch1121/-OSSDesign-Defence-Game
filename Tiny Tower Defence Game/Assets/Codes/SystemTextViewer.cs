using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SystemType { Money = 0, Build = 1, Barrack }
public class SystemTextViewer : MonoBehaviour
{
    private Text textSystem;
    private TextAlpha alpha;

    private void Awake()
    {
        textSystem = GetComponent<Text>();
        alpha = GetComponent<TextAlpha>();
    }

    public void PrintText(SystemType type)
    {
        switch (type)
        {
            case SystemType.Money:
                textSystem.text = "System : Not enough money...";
                break;
            case SystemType.Build:
                textSystem.text = "System : Invalid build tower...";
                break;
            case SystemType.Barrack:
                textSystem.text = "System : Must build Barrack...";
                break;
        }

        alpha.FadeOut();
    }
}

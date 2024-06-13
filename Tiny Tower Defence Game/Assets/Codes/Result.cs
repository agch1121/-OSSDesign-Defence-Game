using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        titles[0].SetActive(true);
    }

    public void Win()
    {
        titles[1].SetActive(true);
    }

    public void WinClose()
    {
        titles[1].SetActive(false);
    }
    public void LoseClose()
    {
        titles[0].SetActive(false);
    }
    public void Finish()
    {
        titles[2].SetActive(true);
    }
}

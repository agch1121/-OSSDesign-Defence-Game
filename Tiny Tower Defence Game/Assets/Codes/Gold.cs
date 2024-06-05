using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private int currentGold = 100;

    public int CurrentGold //set,get으로 외부 접근 가능하도록 함
    {
        set => currentGold = Mathf.Max(0, value);
        get => currentGold;
    }
}

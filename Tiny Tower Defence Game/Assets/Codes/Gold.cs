using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private int currentGold = 100;

    public int CurrentGold //set,get���� �ܺ� ���� �����ϵ��� ��
    {
        set => currentGold = Mathf.Max(0, value);
        get => currentGold;
    }
}

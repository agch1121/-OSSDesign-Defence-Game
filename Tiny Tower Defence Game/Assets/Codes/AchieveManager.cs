using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;
    public Sprite targetSprite;
    public WaveSystem ws;

    enum Achieve { Unlock1, Unlock2, Unlock3, Unlock4 }
    Achieve[] achieves;
    WaitForSecondsRealtime wait;

    private void Awake()
    {
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve)); // 주어진 열거형의 데이터를 모두 가져오는 함수
        wait = new WaitForSecondsRealtime(3);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1); // 간단한 저장 기능을 제공하는 유니티 제공 클래스

        foreach (Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int index = 0; index < lockCharacter.Length; index++)
        {
            string achieveName = achieves[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }

    void CheckAchieve(Achieve achieve)
    {
        bool isAchieve = false;

        if (ws == null)
        {
            Debug.LogError("WaveSystem 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        switch (achieve)
        {
            case Achieve.Unlock1:
                isAchieve = ws.CurrentWave > 1;
                break;
            case Achieve.Unlock2:
                isAchieve = ws.CurrentWave > 4;
                break;
            case Achieve.Unlock3:
                isAchieve = ws.CurrentWave > 7;
                break;
            case Achieve.Unlock4:
                isAchieve = ws.CurrentWave == ws.MaxWave;
                break;
        }

        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);

            for (int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achieve;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
            UnlockCharacter();
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        yield return wait;
        uiNotice.SetActive(false);
    }
}

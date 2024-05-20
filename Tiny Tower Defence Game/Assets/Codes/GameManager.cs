using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Networking.UnityWebRequest;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    // 몬스터 처치시 정보 및 보상
    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int gold;

    [Header("# Game Object")]
    public bool isLive;
    public PoolManager pool;
    public Result uiResult;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60; // 기본 프레임을 60으로 지정
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        //player.gameObject.SetActive(true);
        //uiLevelUp.Select(playerId % 2);
        Resume();

        //AudioManager.instance.PlayBgm(true);
        //AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        //StartCoroutine(GameOverRoutine());
    }

    //IEnumerator GameOverRoutine()
    //{
    //    isLive = false;

    //    yield return new WaitForSeconds(0.5f);

    //    uiResult.gameObject.SetActive(true);
    //    uiResult.Lose();
    //    Stop();

    //    AudioManager.instance.PlayBgm(false);
    //    AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    //}

    //public void GameVictory()
    //{
    //    StartCoroutine(GameVictoryRoutine());
    //}

    //IEnumerator GameVictoryRoutine()
    //{
    //    isLive = false;
    //    enemyCleaner.SetActive(true);

    //    yield return new WaitForSeconds(0.5f);

    //    uiResult.gameObject.SetActive(true);
    //    uiResult.Win();
    //    Stop();

    //    AudioManager.instance.PlayBgm(false);
    //    AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    //}
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            //GameVictory();
        }
    }

    public void getExp()
    {
        if (!isLive)
            return;

        gold++;
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; // 시간 멈춤
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

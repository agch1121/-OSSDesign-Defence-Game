using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Networking.UnityWebRequest;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("# Game Object")]
    public bool isLive;
    public Result uiResult;
    WaitForSecondsRealtime wait;
    public WaveSystem ws;
    public PlayerHP playerHP;
    public Spawner enemySpawner;
    public SaveManager saveManager;
    public Gold playerGold;
    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60; // 기본 프레임을 60으로 지정
        wait = new WaitForSecondsRealtime(3);
    }

    

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        yield return null;
    }
  
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        yield return wait;
        uiResult.WinClose();
        uiResult.gameObject.SetActive(false);

    }
    public void GameEnd()
    {
        uiResult.gameObject.SetActive(true);
        uiResult.Finish();
    }
    public void GameRetry()
    {
        ws.setWave(1);
        playerHP.setHP(10);
        uiResult.gameObject.SetActive(false);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
    public void GameLoad()
    {
        saveManager = FindObjectOfType<SaveManager>();
        GameData loadedData = saveManager.LoadGame();
        if (loadedData != null)
        {
            saveManager.LoadGameDataIntoScene(loadedData);
        }
    }

    public void SaveGame()
    {
        float playerHealth = playerHP.CurrentHP;
        int gold = playerGold.CurrentGold;
        int waveIndex = ws.CurrentWave + 1;
        List<TowerWeapon> towers = FindObjectOfType<TowerSpawner>().GetAllTowers();
        saveManager.SaveGame(playerHealth, gold, waveIndex, towers);
    }
}

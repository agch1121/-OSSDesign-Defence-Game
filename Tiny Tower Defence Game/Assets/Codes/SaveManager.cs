using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public float playerHealth;
    public int gold;
    public int waveIndex;
    public List<TowerData> towers = new List<TowerData>();
}

[System.Serializable]
public class TowerData
{
    public Vector3 position;
    public float attackPower;
    public float attackRange;
    public float attackSpeed;
    public int level;
}

public class SaveManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    public void SaveGame(float playerHealth, int gold, int waveIndex, List<TowerWeapon> towers)
    {
        GameData gameData = new GameData
        {
            playerHealth = playerHealth,
            gold = gold,
            waveIndex = waveIndex
        };

        foreach (var tower in towers)
        {
            TowerData towerData = new TowerData
            {
                position = tower.transform.position,
                attackPower = tower.GetComponent<TowerWeapon>().Damage,
                attackRange = tower.GetComponent<TowerWeapon>().Range,
                attackSpeed = tower.GetComponent<TowerWeapon>().Rate,
                level = tower.Level,
            };
            gameData.towers.Add(towerData);
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);
    }

    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }

    public void LoadGameDataIntoScene(GameData gameData)
    {
        if (gameData == null)
        {
            Debug.LogError("저장된 데이터가 없습니다.");
            return;
        }

        // 플레이어 체력과 골드 설정
        PlayerHP player = FindObjectOfType<PlayerHP>();
        player.loadHP(gameData.playerHealth);
        // 골드를 관리하는 스크립트가 있다고 가정
        Gold playerGold = FindObjectOfType<Gold>();
        playerGold.CurrentGold = gameData.gold;

        // 웨이브 인덱스 설정
        WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
        waveSystem.loadWave(gameData.waveIndex);

    }
}

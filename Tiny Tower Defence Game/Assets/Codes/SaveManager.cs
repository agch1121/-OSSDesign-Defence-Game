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
            Debug.LogError("����� �����Ͱ� �����ϴ�.");
            return;
        }

        // �÷��̾� ü�°� ��� ����
        PlayerHP player = FindObjectOfType<PlayerHP>();
        player.loadHP(gameData.playerHealth);
        // ��带 �����ϴ� ��ũ��Ʈ�� �ִٰ� ����
        Gold playerGold = FindObjectOfType<Gold>();
        playerGold.CurrentGold = gameData.gold;

        // ���̺� �ε��� ����
        WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
        waveSystem.loadWave(gameData.waveIndex);

    }
}

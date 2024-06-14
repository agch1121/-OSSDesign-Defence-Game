using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private Spawner enemySpawner;
    private int currentWaveIndex = -1;

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;
    public void setWave(int waveIndex)
    {
        currentWaveIndex -= waveIndex;
    }
    public void loadWave(int waveIndex)
    {
        currentWaveIndex = waveIndex;
    }
    public void StartWave()
    {
        // 현재 맵에 적이 없고, Wave가 남아있으면
        if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            // 인덱스의 시작이 -1이기 때문에 웨이브 인덱스 증가를 제일 먼저 함
            currentWaveIndex++;
            // EnemySpawner의 StartWave() 함수 호출 현재 웨이브 정보 제공
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}
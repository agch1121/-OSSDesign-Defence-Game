using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private int towerBuildCost = 50;
    [SerializeField]
    private Spawner enemySpawner;
    [SerializeField]
    private Gold playerGold;

    public void SpawnTower(Transform tileTransform)
    {
        if (towerBuildCost > playerGold.CurrentGold)
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();


        // 2. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if (tile.IsBuildTower == true)
        {
            return;
        }
        // 타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= towerBuildCost;
        GameObject clone = Instantiate(towerPrefab[Random.Range(0,2)], tileTransform.position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
    
}

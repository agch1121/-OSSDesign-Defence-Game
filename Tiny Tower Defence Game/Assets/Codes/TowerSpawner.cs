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


        // 2. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� x
        if (tile.IsBuildTower == true)
        {
            return;
        }
        // Ÿ���� �Ǽ��Ǿ� �������� ����
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= towerBuildCost;
        GameObject clone = Instantiate(towerPrefab[Random.Range(0,2)], tileTransform.position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
    
}

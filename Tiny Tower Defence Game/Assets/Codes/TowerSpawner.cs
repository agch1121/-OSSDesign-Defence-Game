using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate template;
    [SerializeField]
    private Spawner enemySpawner;
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    private SystemTextViewer textViewer;

    public void SpawnTower(Transform tileTransform)
    {
        if (template.weapon[0].cost > playerGold.CurrentGold)
        {
            textViewer.PrintText(SystemType.Money);
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();


        // 2. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if (tile.IsBuildTower == true)
        {
            textViewer.PrintText(SystemType.Build);
            return;
        }
        // 타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= template.weapon[0].cost;
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(template.towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);
    }
    
}

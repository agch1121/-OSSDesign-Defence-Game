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


        // 2. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� x
        if (tile.IsBuildTower == true)
        {
            textViewer.PrintText(SystemType.Build);
            return;
        }
        // Ÿ���� �Ǽ��Ǿ� �������� ����
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= template.weapon[0].cost;
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(template.towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);
    }
    
}

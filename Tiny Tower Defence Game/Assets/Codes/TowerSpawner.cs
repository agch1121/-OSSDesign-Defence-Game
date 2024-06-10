using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] template;
    [SerializeField]
    private Spawner enemySpawner;
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    private SystemTextViewer textViewer;

    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;
    private int towerType;

    public void ReadyToSpawnTower(int type)
    {
        towerType = type;
        if(isOnTowerButton == true)
        {
            return;
        }
        if (template[towerType].weapon[0].cost > playerGold.CurrentGold)
        {
            textViewer.PrintText(SystemType.Money);
            return;
        }

        isOnTowerButton = true;
        followTowerClone = Instantiate(template[towerType].followTowerPrefabs);
        StartCoroutine("OnTowerCancelSystem");
    }
    public void SpawnTower(Transform tileTransform)
    {
        if(isOnTowerButton == false)
        {
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
        isOnTowerButton = false;
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= template[towerType].weapon[0].cost;
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(template[towerType].towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);

        Destroy(followTowerClone);
        StopCoroutine("OnTowerCancelSystem");
    }
    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            // ESCŰ �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �Ǽ� ���
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}

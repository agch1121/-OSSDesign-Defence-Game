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

        // 2. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if (tile.IsBuildTower == true)
        {
            textViewer.PrintText(SystemType.Build);
            return;
        }
        // 타워가 건설되어 있음으로 설정
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
            // ESC키 또는 마우스 오른쪽 버튼을 눌렀을 때 타워 건설 취소
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                // 마우스를 따라다니는 임시 타워 삭제
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}

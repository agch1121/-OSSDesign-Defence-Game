using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private Spawner enemySpawner;

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();


        // 2. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if (tile.IsBuildTower == true)
        {
            return;
        }
        // 타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;
        GameObject clone = Instantiate(towerPrefab[Random.Range(0,2)], tileTransform.position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHPSliderPrefabs;
    [SerializeField]
    private Transform canvasTransform;
    public Transform[] waypoints;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private Gold playerGold;
    private Wave currentWave;

    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy>();
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            int index = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[index]);
            Enemy enemyScript = clone.GetComponent<Enemy>();
            enemyScript.wayPoints = waypoints;

            enemyScript.Setup(this, waypoints);
            enemyList.Add(enemyScript);

            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;
            yield return new WaitForSeconds(currentWave.spawnTime);
        }    
    }
    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        if (type == EnemyDestroyType.Arrive)
        {
            // 플레이어의 체력 -1
            playerHP.TakeDamage(1);
        }
        else if(type == EnemyDestroyType.Kill)
        {
            playerGold.CurrentGold += gold;
        }
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // 적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefabs);
        // Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        // Tip. UI는 캔버스의 자식오브젝트로 설정되어 있어야 화면에 보인다.
        sliderClone.transform.SetParent(canvasTransform);
        // 계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI가 쫒아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderAutoPosition>().Setup(enemy.transform);
        // Slider UI에 자신(enemy)의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<HPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}

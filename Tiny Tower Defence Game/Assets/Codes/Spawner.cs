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
            // �÷��̾��� ü�� -1
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
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefabs);
        // Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        // Tip. UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�.
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1,1,1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI�� �i�ƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderAutoPosition>().Setup(enemy.transform);
        // Slider UI�� �ڽ�(enemy)�� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<HPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}

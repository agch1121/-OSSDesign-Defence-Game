using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform spawnPoint;
    public Transform[] waypoints;
    public string enemyTag;
    public float spawnInterval = 2f;
    public int maxSpawnCount = 10;

    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    private int currentSpawnCount = 0;

    private void Awake()
    {
        enemyList = new List<Enemy>();
    }
    void Start()
    {
       InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentSpawnCount < maxSpawnCount)
        {
            int index = Random.Range(0, enemyPrefab.Length);
            GameObject enemy = Instantiate(enemyPrefab[index], spawnPoint.position, spawnPoint.rotation);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.wayPoints = waypoints;

            enemyScript.Setup(this, waypoints);
            enemyList.Add(enemyScript);
        }
        else
        {
            CancelInvoke(nameof(SpawnEnemy));
        }
        currentSpawnCount++;
    }
    public void DestroyEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
//    public Transform[] spawnPoint;
//    public SpawnData[] spawnData;
//    public float levelTime;
//    [SerializeField]
//    private Transform[] wayPoints; // ���� ���������� �̵� ���
//    int level;
//    float timer;
//    void Awake()
//    {
//        spawnPoint = GetComponentsInChildren<Transform>();
//        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!GameManager.instance.isLive)
//            return;
//        timer += Time.deltaTime;
//        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1); // �Ҽ��� ������ ���������� ��ȯ
//        // CeilToInt : ������ �Ʒ��� �ݿø��ϰ� ���������� ��ȯ
//        //if (timer > spawnData[level].spawnTime)
//        if(timer > 0.2f)
//        {
//            timer = 0f;
//            Spawn();
//        }
//    }

//    void Spawn()
//    {
//        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 4));
//        // �ڽ� ������Ʈ������ ���õǵ��� ���� ������ 1���� ����
//        enemy.transform.position = spawnPoint[1].position;
//        enemy.GetComponent<Enemy>().Init(spawnData[level]);
//        int spawnEnemyCount = 0;
//        Enemy enemy1 = enemy.GetComponent<Enemy>();
//        while (spawnEnemyCount < 10)
//        {
//            //GameObject clone = Instantiate(enemyPrefabs); // �� ������Ʈ ����
//            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ����
//            // this�� �� �ڽ� (�ڽ��� EnemySpawner ����)
//            enemy1.Setup(wayPoints); // wayPoint(�̵����) ������ �Ű������� Setup() ȣ��

//            // ���� ���̺꿡�� ������ ���� ����+1
//            spawnEnemyCount++;

//        }
//    }
//}
//[System.Serializable]
//public class SpawnData
//{
//    public float spawnTime;
//    public int spriteType;
//    public int health;
//    public float speed;
//}

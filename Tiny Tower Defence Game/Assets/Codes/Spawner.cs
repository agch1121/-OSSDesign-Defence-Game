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
//    private Transform[] wayPoints; // 현재 스테이지의 이동 경로
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
//        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1); // 소수점 버리고 정수형으로 변환
//        // CeilToInt : 수수점 아래를 반올림하고 정수형으로 변환
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
//        // 자식 오브젝트에서만 선택되도록 랜덤 시작은 1부터 시작
//        enemy.transform.position = spawnPoint[1].position;
//        enemy.GetComponent<Enemy>().Init(spawnData[level]);
//        int spawnEnemyCount = 0;
//        Enemy enemy1 = enemy.GetComponent<Enemy>();
//        while (spawnEnemyCount < 10)
//        {
//            //GameObject clone = Instantiate(enemyPrefabs); // 적 오브젝트 생성
//            // 웨이브에 등장하는 적의 종류가 여러 종류일 때 임의의 적이 등장하도록 설정하고, 적 오브젝트 생성
//            // this는 나 자신 (자신의 EnemySpawner 정보)
//            enemy1.Setup(wayPoints); // wayPoint(이동경로) 정보를 매개변수로 Setup() 호출

//            // 현재 웨이브에서 생성한 적의 숫자+1
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

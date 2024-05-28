using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform[] wayPoints;
    private int waypointIndex = 0;

    public float speed;
    public float health;
    public float maxHealth;

    bool isLive;


    void Awake()
    {
  
    }
    void OnEnable()
    {
        waypointIndex = 0;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex < wayPoints.Length)
        {
            Transform targetWaypoint = wayPoints[waypointIndex];
            Vector3 direction = targetWaypoint.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (transform.position == targetWaypoint.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            gameObject.SetActive(false); // ��� ��������Ʈ�� ������ ��Ȱ��ȭ
        }
    }
    //// Update is called once per frame
    //void FixedUpdate()
    //{

    //    Vector2 dirVec = target.position - rigid.position;
    //    Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
    //    rigid.MovePosition(rigid.position + nextVec);
    //    rigid.velocity = Vector2.zero;
    //    if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
    //    {
    //        if (currentIndex < wayPointCount - 1)
    //        {
    //            // ���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
    //            transform.position = wayPoints[currentIndex].position;
    //            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
    //            currentIndex++;
    //            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
    //            movement2D.MoveTo(direction);
    //        }
    //    }
    //}

    //private void LateUpdate()
    //{

    //}

    //public void Setup(Transform[] wayPoints)
    //{
    //    movement2D = GetComponent<Movement2D>();

    //    // �� �̵� ��� WayPoints ��� ����
    //    wayPointCount = wayPoints.Length;
    //    this.wayPoints = new Transform[wayPointCount];
    //    this.wayPoints = wayPoints;

    //    // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
    //    transform.position = wayPoints[currentIndex].position;

    //    // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
    //    StartCoroutine("OnMove");
    //}

    //IEnumerator OnMove()
    //{
    //    // ���� �̵� ���� ����
    //    NextMoveTo();

    //    while (true)
    //    {
    //        // �� ������Ʈ Ȯ��
    //        transform.Rotate(Vector3.forward * 10);

    //        // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02 * movement2D.MoveSpeed ���� ������ if ���ǹ� ����
    //        // Tip. movement2D.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ����
    //        // if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� ����.
    //        if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
    //            NextMoveTo(); // ���� �̵� ���� ����

    //        yield return null;
    //    }
    //}

    //void NextMoveTo()
    //{
    //    // ���� �̵��� wayPoitns�� �����ִٸ�
    //    if (currentIndex < wayPointCount - 1)
    //    {
    //        // ���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
    //        transform.position = wayPoints[currentIndex].position;
    //        // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
    //        currentIndex++;
    //        Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
    //        movement2D.MoveTo(direction);
    //    }
    //}
}
    //    public void Init(SpawnData data)
    //{
    //    anim.runtimeAnimatorController = animCon[data.spriteType];
    //    speed = data.speed; 
    //    health = data.health;
    //    maxHealth = data.health;
    //}

//    void Dead()
//    {
//        gameObject.SetActive(false);
//    }
//}

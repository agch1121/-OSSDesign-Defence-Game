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
            gameObject.SetActive(false); // 모든 웨이포인트를 지나면 비활성화
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
    //            // 적의 위치를 정확하게 목표 위치로 설정
    //            transform.position = wayPoints[currentIndex].position;
    //            // 이동 방향 설정 => 다음 목표지점(wayPoints)
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

    //    // 적 이동 경로 WayPoints 경로 설정
    //    wayPointCount = wayPoints.Length;
    //    this.wayPoints = new Transform[wayPointCount];
    //    this.wayPoints = wayPoints;

    //    // 적의 위치를 첫번째 wayPoint 위치로 설정
    //    transform.position = wayPoints[currentIndex].position;

    //    // 적 이동/목표지점 설정 코루틴 함수 시작
    //    StartCoroutine("OnMove");
    //}

    //IEnumerator OnMove()
    //{
    //    // 다음 이동 방향 설정
    //    NextMoveTo();

    //    while (true)
    //    {
    //        // 적 오브젝트 확인
    //        transform.Rotate(Vector3.forward * 10);

    //        // 적의 현재위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed 보다 작을때 if 조건문 실행
    //        // Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문
    //        // if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있음.
    //        if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
    //            NextMoveTo(); // 다음 이동 방향 설정

    //        yield return null;
    //    }
    //}

    //void NextMoveTo()
    //{
    //    // 아직 이동할 wayPoitns가 남아있다면
    //    if (currentIndex < wayPointCount - 1)
    //    {
    //        // 적의 위치를 정확하게 목표 위치로 설정
    //        transform.position = wayPoints[currentIndex].position;
    //        // 이동 방향 설정 => 다음 목표지점(wayPoints)
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

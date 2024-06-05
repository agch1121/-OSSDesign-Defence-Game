using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType { Kill = 0, Arrive }
public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    public Transform[] wayPoints;
    private int waypointIndex = 0;

    private Spawner enemySpawner;
    private Movement2D movement2D;

    [SerializeField]
    private int gold = 10;

    bool isLive;


    public void Setup(Spawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // 적 이동 경로 WayPoints 경로 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[waypointIndex].position;
        StartCoroutine("OnMove");
    }

    IEnumerator OnMove()
    {
        // 다음 이동 방향 설정
        NextMoveTo();

        while (true)
        {
            // 적 오브젝트 확인
            //transform.Rotate(Vector3.forward * 10);

            // 적의 현재위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed 보다 작을때 if 조건문 실행
            // Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문
            // if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있음.
            if (Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < 0.02f * movement2D.MoveSpeed)
                NextMoveTo(); // 다음 이동 방향 설정

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // 아직 이동할 wayPoitns가 남아있다면
        if (waypointIndex < wayPointCount - 1)
        {
            // 적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[waypointIndex].position;
            // 이동 방향 설정 => 다음 목표지점(wayPoints)
            waypointIndex++;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // 현재 위치가 마지막 wayPoints이면
        else
        {
            gold = 0;
            // 목표지점에 도달해서 사망할 때는 돈을 주지 않도록 설정
            // 적 오브젝트 삭제
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}

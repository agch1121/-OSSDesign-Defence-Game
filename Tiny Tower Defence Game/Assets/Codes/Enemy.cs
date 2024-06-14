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

    bool isStopped; // 적의 이동이 멈췄는지 여부

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
            // 이동이 멈춘 상태라면 루프를 계속 돌면서 대기
            if (isStopped)
            {
                yield return null;
                continue;
            }

            // 적의 현재위치와 목표위치의 거리가 0.05 * movement2D.MoveSpeed 보다 작을 때 if 조건문 실행
            if (Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < 0.05f * movement2D.MoveSpeed)
                NextMoveTo(); // 다음 이동 방향 설정

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // 아직 이동할 wayPoints가 남아있다면
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 유닛과 충돌했을 때
        if (collision.gameObject.CompareTag("Unit"))
        {
            // 이동 멈춤
            isStopped = true;
            movement2D.Stop(); // Movement2D 클래스에서 이동을 멈추는 메서드가 필요합니다.
            collision.GetComponent<UnitHP>().TakeDamage(2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            changeState();
            return;
        }
    }
    void changeState()
    {
        // Idle 상태로 전환
        movement2D.ResetMoveSpeed();
    }
}

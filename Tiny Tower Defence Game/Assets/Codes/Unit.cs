using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Collider2D collidedEnemy;
    public float moveSpeed = 1f; // 이동 속도
    private Animator animator;

    public Transform[] wayPoints;
    private UnitSpawner unitSpawner;
    private int waypointIndex = 0;
    private int wayPointCount;
    private Movement2D movement2D;
    private bool isStopped = false;
    private int damage;
    private bool turnBack = false;

    void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    public void Setup(UnitSpawner unitSpawner, Transform[] wayPoints, int damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.unitSpawner = unitSpawner;
        this.damage = damage;

        // 유닛 이동 경로 WayPoints 경로 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        
        transform.position = wayPoints[waypointIndex].position;
        StartCoroutine("OnMove");
    }


    IEnumerator OnMove()
    {
        NextMoveTo();
        while (true)
        {
            // 이동이 멈춘 상태라면 루프를 계속 돌면서 대기
            if (isStopped)
            {
                yield return null;
                continue;
            }

            // 유닛의 현재위치와 목표위치의 거리가 0.05 * movement2D.MoveSpeed 보다 작을 때 if 조건문 실행
            if (Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < 0.05f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }
    void NextMoveTo()
    {
        // 웨이포인트 인덱스가 마지막 인덱스보다 작고 turnBack이 false일 경우
        if (waypointIndex < wayPointCount - 1 && !turnBack)
        {
            // 적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[waypointIndex].position;
            // 이동 방향 설정 => 다음 목표지점(wayPoints)
            waypointIndex++;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // 현재 위치가 마지막 wayPoints이면
        else if (waypointIndex == wayPointCount - 1 && !turnBack)
        {
            // turnBack 플래그 설정
            turnBack = true;
            // 이동 방향 설정 => 이전 목표지점(wayPoints)
            waypointIndex--;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // turnBack이 true일 경우 (반대로 이동 중)
        else if (turnBack)
        {
            // 이동 방향 설정 => 이전 목표지점(wayPoints)
            waypointIndex--;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);

            // 시작지점에 도달하면 turnBack 플래그 초기화
            if (waypointIndex == 0)
            {
                turnBack = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return; // 적이 아닌 대상과 부딪히면
        }
        // 적과 충돌했을 때
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // 움직임 멈춤
            movement2D.Stop();
            // 공격 상태로 전환
            animator.SetTrigger("doAttack");
            StartCoroutine(AttackDelay(collision));
            collidedEnemy = collision; // 부딪힌 적 오브젝트의 Collider2D 저장
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision == null)
        {
            changeState();
            return;
        }
    }
    void changeState()
    {
            // Idle 상태로 전환
            movement2D.ResetMoveSpeed();
            animator.SetTrigger("doMove");
            collidedEnemy = null; // 부딪힌 적 오브젝트의 Collider2D 초기화
        
    }

    private IEnumerator AttackDelay(Collider2D collision) {
        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("doStop");
    }
    public void OnDie()
    {
        unitSpawner.DestroyUnit(this);
    }

}

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

    bool isStopped; // ���� �̵��� ������� ����

    public void Setup(Spawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();

        this.enemySpawner = enemySpawner;

        // �� �̵� ��� WayPoints ��� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
        transform.position = wayPoints[waypointIndex].position;
        StartCoroutine("OnMove");
    }

    IEnumerator OnMove()
    {
        // ���� �̵� ���� ����
        NextMoveTo();

        while (true)
        {
            // �̵��� ���� ���¶�� ������ ��� ���鼭 ���
            if (isStopped)
            {
                yield return null;
                continue;
            }

            // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.05 * movement2D.MoveSpeed ���� ���� �� if ���ǹ� ����
            if (Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < 0.05f * movement2D.MoveSpeed)
                NextMoveTo(); // ���� �̵� ���� ����

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // ���� �̵��� wayPoints�� �����ִٸ�
        if (waypointIndex < wayPointCount - 1)
        {
            // ���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[waypointIndex].position;
            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
            waypointIndex++;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // ���� ��ġ�� ������ wayPoints�̸�
        else
        {
            gold = 0;
            // ��ǥ������ �����ؼ� ����� ���� ���� ���� �ʵ��� ����
            // �� ������Ʈ ����
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, gold);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���ְ� �浹���� ��
        if (collision.gameObject.CompareTag("Unit"))
        {
            // �̵� ����
            isStopped = true;
            movement2D.Stop(); // Movement2D Ŭ�������� �̵��� ���ߴ� �޼��尡 �ʿ��մϴ�.
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
        // Idle ���·� ��ȯ
        movement2D.ResetMoveSpeed();
    }
}

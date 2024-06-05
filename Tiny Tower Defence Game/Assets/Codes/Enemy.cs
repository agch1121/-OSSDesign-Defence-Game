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
            // �� ������Ʈ Ȯ��
            //transform.Rotate(Vector3.forward * 10);

            // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02 * movement2D.MoveSpeed ���� ������ if ���ǹ� ����
            // Tip. movement2D.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ����
            // if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� ����.
            if (Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < 0.02f * movement2D.MoveSpeed)
                NextMoveTo(); // ���� �̵� ���� ����

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // ���� �̵��� wayPoitns�� �����ִٸ�
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
}

using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Collider2D collidedEnemy;
    public float moveSpeed = 1f; // �̵� �ӵ�
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
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    public void Setup(UnitSpawner unitSpawner, Transform[] wayPoints, int damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.unitSpawner = unitSpawner;
        this.damage = damage;

        // ���� �̵� ��� WayPoints ��� ����
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
            // �̵��� ���� ���¶�� ������ ��� ���鼭 ���
            if (isStopped)
            {
                yield return null;
                continue;
            }

            // ������ ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.05 * movement2D.MoveSpeed ���� ���� �� if ���ǹ� ����
            if (Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < 0.05f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }
    void NextMoveTo()
    {
        // ��������Ʈ �ε����� ������ �ε������� �۰� turnBack�� false�� ���
        if (waypointIndex < wayPointCount - 1 && !turnBack)
        {
            // ���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[waypointIndex].position;
            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
            waypointIndex++;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // ���� ��ġ�� ������ wayPoints�̸�
        else if (waypointIndex == wayPointCount - 1 && !turnBack)
        {
            // turnBack �÷��� ����
            turnBack = true;
            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
            waypointIndex--;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // turnBack�� true�� ��� (�ݴ�� �̵� ��)
        else if (turnBack)
        {
            // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
            waypointIndex--;
            Vector3 direction = (wayPoints[waypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);

            // ���������� �����ϸ� turnBack �÷��� �ʱ�ȭ
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
            return; // ���� �ƴ� ���� �ε�����
        }
        // ���� �浹���� ��
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // ������ ����
            movement2D.Stop();
            // ���� ���·� ��ȯ
            animator.SetTrigger("doAttack");
            StartCoroutine(AttackDelay(collision));
            collidedEnemy = collision; // �ε��� �� ������Ʈ�� Collider2D ����
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
            // Idle ���·� ��ȯ
            movement2D.ResetMoveSpeed();
            animator.SetTrigger("doMove");
            collidedEnemy = null; // �ε��� �� ������Ʈ�� Collider2D �ʱ�ȭ
        
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

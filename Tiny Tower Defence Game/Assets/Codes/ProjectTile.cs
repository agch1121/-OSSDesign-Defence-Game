using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private int damage;

    public void Setup(Transform target, int damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;  // Ÿ���� �������� target
        this.damage = damage;
    }
    void Update()
    {
        if (target != null) // target�� �����ϸ�
        {
            // �߻�ü�� target�� ��ġ�� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;

            //currentRotation.z = Mathf.LerpAngle(currentRotation.z, angle, Time.deltaTime * 2);

            //transform.eulerAngles = currentRotation;
        }
        else // ���� ������ target�� �������
        {
            // �߻�ü ������Ʈ ����
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; // ���� �ƴ� ���� �ε�����
        if (collision.transform != target) return; // ���� target�� ���� �ƴ� ��

        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Destroy(gameObject); // �߻�ü ������Ʈ ����
    }
}

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
        this.target = target;  // 타워가 설정해준 target
        this.damage = damage;
    }
    void Update()
    {
        if (target != null) // target이 존재하면
        {
            // 발사체를 target의 위치로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;

            //currentRotation.z = Mathf.LerpAngle(currentRotation.z, angle, Time.deltaTime * 2);

            //transform.eulerAngles = currentRotation;
        }
        else // 여러 이유로 target이 사라지면
        {
            // 발사체 오브젝트 삭제
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; // 적이 아닌 대상과 부딪히면
        if (collision.transform != target) return; // 현재 target이 적이 아닐 때

        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Destroy(gameObject); // 발사체 오브젝트 삭제
    }
}

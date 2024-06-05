using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projecttilePrefab;
    //[SerializeField]
    //private TowerTemplate towerTemplate; // 타워 정보 (공격력, 공격속도 등)
    [SerializeField]
    private Transform spawnPoint; // 발사체 생성 위치
    [SerializeField]
    private float attackRate = 0.5f; // 공격 속도
    [SerializeField]
    private float attackRange = 2.0f; // 공격 범위
    [SerializeField]
    private int attackDamage = 1;
    private WeaponState weaponState = WeaponState.SearchTarget; // 타워 무기의 상태
    private Transform attackTarget = null; // 공격 대상
    private Spawner enemySpawner; // 게임에 존재하는 적 정보 획득용

    public void Setup(Spawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        // 이전에 재생중이던 상태 종료
        StopCoroutine(weaponState.ToString());
        // 상태 변경
        weaponState = newState;
        // 새로운 상태 재생
        StartCoroutine(weaponState.ToString());
    }
    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
            float closestDistSqr = Mathf.Infinity;
            // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // 현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                if (distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }
            if(attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
    }
    private IEnumerator AttackToTarget()
    {
        while(true)
        {
            if(attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(attackRate);

            SpawnProjectTile();
        }
    }

    private void SpawnProjectTile()
    {
        GameObject clone = Instantiate(projecttilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<ProjectTile>().Setup(attackTarget, attackDamage);
    }
}

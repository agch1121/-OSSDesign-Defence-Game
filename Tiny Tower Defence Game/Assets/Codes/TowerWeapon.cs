using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate template;
    [SerializeField]
    private GameObject projecttilePrefab;
    [SerializeField]
    private Transform spawnPoint; // �߻�ü ���� ��ġ
    [SerializeField]
    private int level = 0;
    private WeaponState weaponState = WeaponState.SearchTarget; // Ÿ�� ������ ����
    private Transform attackTarget = null; // ���� ���
    private SpriteRenderer spriteRenderer;
    private Spawner enemySpawner; // ���ӿ� �����ϴ� �� ���� ȹ���
    private Gold playerGold;
    private Tile ownerTile;

    public Sprite TowerSprite => template.weapon[level].sprite;
    public int Damage => template.weapon[level].damage;
    public float Rate => template.weapon[level].rate;
    public float Range => template.weapon[level].range;
    public int Level => level + 1;
    public int MaxLevel => template.weapon.Length;
    public void Setup(Spawner enemySpawner, Gold playerGold, Tile ownerTile)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;

        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        // ������ ������̴� ���� ����
        StopCoroutine(weaponState.ToString());
        // ���� ����
        weaponState = newState;
        // ���ο� ���� ���
        StartCoroutine(weaponState.ToString());
    }
    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float closestDistSqr = Mathf.Infinity;
            // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
                if (distance <= template.weapon[level].range && distance <= closestDistSqr)
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
            if(distance > template.weapon[level].range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(template.weapon[level].rate);

            SpawnProjectTile();
        }
    }

    private void SpawnProjectTile()
    {
        GameObject clone = Instantiate(projecttilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<ProjectTile>().Setup(attackTarget, template.weapon[level].damage);
    }
    public bool Upgrade()
    {
        // Ÿ�� ���׷��̵忡 �ʿ��� ��尡 ������� �˻�
        if (playerGold.CurrentGold < template.weapon[level + 1].cost)
        {
            return false;
        }

        // Ÿ�� ���� ����
        level++;
        // Ÿ�� ���� ����(Sprite)
        spriteRenderer.sprite = template.weapon[level].sprite;
        // ��� ����
        playerGold.CurrentGold -= template.weapon[level].cost;

        return true;
    }
    public void Sell()
    {
        // ��� ����
        playerGold.CurrentGold += template.weapon[level].sell;
        // ���� Ÿ�Ͽ� �ٽ� Ÿ�� �Ǽ��� �����ϵ��� ����
        ownerTile.IsBuildTower = false;
        // Ÿ�� �ı�
        Destroy(gameObject);
    }
}

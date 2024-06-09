using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    private void Awake()
    {
        OffAttackRange();
    }
    public void OnAttackRange(Vector3 position, float range)
    {
        gameObject.SetActive(true);

        // 공격 범위 크기
        float diameter = range * 2.0f; // range는 반지름이므로 지름 크기를 구함
        transform.localScale = Vector3.one * diameter;
        // 공격 범위 위치
        transform.position = position;
    }
    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}

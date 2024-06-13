using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private float baseMoveSpeed;

    //public float MoveSpeed => moveSpeed;
    // moveSpeed 변수의 프로퍼티 (Set, Get 가능)
    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Max(0, value); // 이동속도가 음수가 되지 않도록 설정
        get => moveSpeed;
    }
    private void Start()
    {
        baseMoveSpeed = MoveSpeed;
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void Stop()
    {
        moveSpeed = 0f;
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = baseMoveSpeed;
    }
}

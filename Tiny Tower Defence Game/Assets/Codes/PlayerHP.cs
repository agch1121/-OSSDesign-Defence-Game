using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image imageScreen; // ��ü ȭ���� ���� ������ �̹���
    [SerializeField]
    private float maxHP = 10;
    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        // ���� ü���� damage��ŭ ����
        currentHP -= damage;
        // ü���� 0�� �Ǹ� ���ӿ���
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        if (currentHP <= 0)
        {

        }
    }
    private IEnumerator HitAlphaAnimation()
    {
        // ��üȭ�� ũ��� ��ġ�� imageScreen�� ������ color ������ ����
        // imageScreen�� ������ 40%�� ����
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        // ������ 0%�� �ɶ����� ����
        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}

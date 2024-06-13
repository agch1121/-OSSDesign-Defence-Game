using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image imageScreen; // 전체 화면을 덮는 빨간색 이미지
    [SerializeField]
    private float maxHP = 10;
    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }
    public void setHP(float hp)
    {
        maxHP += hp;
        currentHP += hp;
    }
    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;
        // 체력이 0이 되면 게임오버
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        if (currentHP <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
    private IEnumerator HitAlphaAnimation()
    {
        // 전체화면 크기로 배치된 imageScreen의 색상을 color 변수에 저장
        // imageScreen의 투명도를 40%로 설정
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        // 투명도가 0%가 될때까지 감소
        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}

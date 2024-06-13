using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public Button skillButton;
    public Image skillImage; // 스프라이트 이미지의 RectTransform
    public Vector3 startPosition; // 시작 위치
    public Vector3 endPosition; // 끝 위치
    public Vector3 startScale; // 시작 스케일
    public Vector3 endScale; // 끝 스케일
    public float duration = 2f; // 이동 및 스케일 변경 시간

    private bool isAnimating = false;
    private float elapsedTime = 0f;
    private int count = 0;

    void Start()
    {
        skillImage.gameObject.SetActive(false); // 시작 시 비활성화
    }

    public void StartSkillEffect()
    {
        if(count >= 5) {
            skillButton.image.color = new Color32(91, 91, 91, 255);
            skillButton.interactable = false;
        }
        skillImage.gameObject.SetActive(true);
        skillImage.rectTransform.localPosition = startPosition;
        skillImage.rectTransform.localScale = startScale;
        isAnimating = true;
        elapsedTime = 0f;

        ApplyDamageToAllEnemies();
        count++;
    }

    void Update()
    {
        if (isAnimating)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            if (t < 1f)
            {
                // 위치와 스케일을 보간하여 변경
                skillImage.rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                skillImage.rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            }
            else
            {
                // 애니메이션 종료
                skillImage.gameObject.SetActive(false);
                isAnimating = false;
            }
        }
    }
    void ApplyDamageToAllEnemies()
    {
        // "Enemy" 태그를 가진 모든 게임 오브젝트를 찾음
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // Enemy 스크립트를 가져와서 데미지 적용
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.GetComponent<EnemyHP>().TakeDamage(10);
            }
        }
    }
}

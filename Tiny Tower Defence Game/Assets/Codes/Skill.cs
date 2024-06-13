using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public Button skillButton;
    public Image skillImage; // ��������Ʈ �̹����� RectTransform
    public Vector3 startPosition; // ���� ��ġ
    public Vector3 endPosition; // �� ��ġ
    public Vector3 startScale; // ���� ������
    public Vector3 endScale; // �� ������
    public float duration = 2f; // �̵� �� ������ ���� �ð�

    private bool isAnimating = false;
    private float elapsedTime = 0f;
    private int count = 0;

    void Start()
    {
        skillImage.gameObject.SetActive(false); // ���� �� ��Ȱ��ȭ
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
                // ��ġ�� �������� �����Ͽ� ����
                skillImage.rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                skillImage.rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            }
            else
            {
                // �ִϸ��̼� ����
                skillImage.gameObject.SetActive(false);
                isAnimating = false;
            }
        }
    }
    void ApplyDamageToAllEnemies()
    {
        // "Enemy" �±׸� ���� ��� ���� ������Ʈ�� ã��
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // Enemy ��ũ��Ʈ�� �����ͼ� ������ ����
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.GetComponent<EnemyHP>().TakeDamage(10);
            }
        }
    }
}

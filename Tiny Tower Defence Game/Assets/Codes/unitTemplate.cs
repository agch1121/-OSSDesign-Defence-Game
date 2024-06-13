using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class unitTemplate : ScriptableObject
{
    public GameObject unitPrefab; // Ÿ�� ������ ���� ������
    public Weapon[] weapon; // ������ Ÿ��(����) ����

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite; // �������� Ÿ�� �̹���(UI)
        public int damage; // ���ݷ�
        public float rate; // ���� �ӵ�
        public float range; // ���� ����
        public int cost; // �ʿ� ���(0���� : �Ǽ�, 1~���� : ���׷��̵�)
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    
    public GameObject Get(int index)
    {
        GameObject select = null;
        // ������ Ǯ�� ��� �ִ� (��Ȱ��ȭ ��) ���ӿ�����Ʈ ����
        // �߰��ϸ� select ������ �Ҵ�

        foreach (GameObject item in pools[index]) // �迭, ����Ʈ�� �����͸� ���������� �����ϴ� �ݺ��� = �ڹ��� Iterator
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // �� ã������?
        // ���Ӱ� �����ϰ� select ������ �Ҵ�
        if (!select)
        {
            // ���� ������Ʈ�� �����Ͽ� ��鿡 �����ϴ� �Լ� : Instantiate
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}

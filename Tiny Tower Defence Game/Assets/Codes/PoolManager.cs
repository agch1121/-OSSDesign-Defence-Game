using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들
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
        // 선택한 풀의 놀고 있는 (비활성화 된) 게임오브젝트 접근
        // 발견하면 select 변수에 할당

        foreach (GameObject item in pools[index]) // 배열, 리스트의 데이터를 순차적으로 접근하는 반복문 = 자바의 Iterator
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못 찾았으면?
        // 새롭게 생성하고 select 변수에 할당
        if (!select)
        {
            // 원본 오브젝트를 복제하여 장면에 생성하는 함수 : Instantiate
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}

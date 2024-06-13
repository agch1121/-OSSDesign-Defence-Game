using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private unitTemplate[] template;
    [SerializeField]
    private GameObject unitHPSliderPrefabs;
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    private Transform canvasTransform;
    public Transform[] waypoints;
    private List<Unit> unitList;
    [SerializeField]
    private SystemTextViewer textViewer;
    private int unitType;
    private int level;
    public List<Unit> UnitList => unitList;
    public int Level => level + 1;
    private void Awake()
    {
        unitList = new List<Unit>();
    }
    public void ReadyToSpawnUnit(int type)
    {
        unitType = type;
        if (template[unitType].weapon[0].cost > playerGold.CurrentGold)
        {
            textViewer.PrintText(SystemType.Money);
            return;
        }
        else
        {
            playerGold.CurrentGold -= template[unitType].weapon[0].cost;
            SpawnUnit(unitType);
        }
    }
    public void SpawnUnit(int type)
    {
        GameObject clone = Instantiate(template[type].unitPrefab);
        ParticleSystem particleSystem = clone.GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        Unit unit = clone.GetComponent<Unit>();
        unit.wayPoints = waypoints;

        unit.Setup(this, waypoints, template[type].weapon[level].damage);
        unitList.Add(unit);

        SpawnUnitHPSlider(clone);
    }
    public void DestroyUnit(Unit unit)
    {
        unitList.Remove(unit);
        Destroy(unit.gameObject);
        Destroy(unit.GetComponentInChildren<Unit>());
    }

    private void SpawnUnitHPSlider(GameObject unit)
    {
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(unitHPSliderPrefabs);
        // Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        // Tip. UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�.
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1,1,1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI�� �i�ƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderAutoPosition>().Setup(unit.transform);
        // Slider UI�� �ڽ�(enemy)�� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<UnitHPViewer>().Setup(unit.GetComponent<UnitHP>());

    }
    
}

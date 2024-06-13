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
        // 적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(unitHPSliderPrefabs);
        // Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        // Tip. UI는 캔버스의 자식오브젝트로 설정되어 있어야 화면에 보인다.
        sliderClone.transform.SetParent(canvasTransform);
        // 계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI가 쫒아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderAutoPosition>().Setup(unit.transform);
        // Slider UI에 자신(enemy)의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<UnitHPViewer>().Setup(unit.GetComponent<UnitHP>());

    }
    
}

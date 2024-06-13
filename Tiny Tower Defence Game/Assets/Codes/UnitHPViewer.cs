using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHPViewer : MonoBehaviour
{
    private UnitHP unitHP;
    private Slider hpSlider;

    public void Setup(UnitHP unitHP)
    {
        this.unitHP = unitHP;
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = unitHP.CurrentHP / unitHP.MaxHP;
    }
}

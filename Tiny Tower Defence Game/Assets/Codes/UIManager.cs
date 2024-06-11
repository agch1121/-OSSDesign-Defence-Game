using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text textPlayerHP;
    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Text textWave;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    private WaveSystem waveSystem;


    private void Update()
    {
        textPlayerHP.text = "HP " + playerHP.CurrentHP;
        textGold.text = playerGold.CurrentGold + " G";
        textWave.text = "Wave " + waveSystem.CurrentWave + "/" + waveSystem.MaxWave; 
    }
    
}

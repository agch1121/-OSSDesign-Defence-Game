using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Button ItemButton;
    public Image itemImage; // ��������Ʈ �̹����� RectTransform
    public PlayerHP playerHP;
    void Start()
    {
        itemImage.gameObject.SetActive(false); // ���� �� ��Ȱ��ȭ
    }

    public void UseItem()
    { 
         Invoke("IncreaseHP", 3);
        
    }
    public void IncreaseHP()
    {
        playerHP.setHP(10);
        itemImage.gameObject.SetActive(false);
        ItemButton.image.color = new Color32(91, 91, 91, 255);
        ItemButton.interactable = false;
    }
}

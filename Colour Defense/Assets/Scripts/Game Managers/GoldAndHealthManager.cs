using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldAndHealthManager : MonoBehaviour, IDatapersistence
{
    public float health;
    public float maxHealth = 100;
    public float gold;
    public GameObject healthbar;
    public GameObject healthText;
    public GameObject GoldText;


    private float maxWidth;
    RectTransform rectTransform;
    TextMeshProUGUI healthtextMeshProUGUI;
    TextMeshProUGUI goldtextMeshProUGUI;

    // Start is called before the first frame update
    void Awake()
    {
        healthtextMeshProUGUI = healthText.GetComponent<TextMeshProUGUI>();
        goldtextMeshProUGUI = GoldText.GetComponent<TextMeshProUGUI>();
        rectTransform = healthbar.GetComponent<RectTransform>();
        maxWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2((health / maxHealth) * maxWidth, rectTransform.rect.height);
        healthtextMeshProUGUI.text = (health.ToString() + " / " + maxHealth.ToString());
    }

    public void ReduceHealth(int damage)
    {
        health -= damage;
        UpdateHealthUI();
    }

    public void GainHealth(int heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void UpdateHealthUI()
    {
        rectTransform.sizeDelta = new Vector2((health / maxHealth) * maxWidth, rectTransform.rect.height);
        healthtextMeshProUGUI.text = (health.ToString() + " / " + maxHealth.ToString());
    }
    
    private void UpdateGoldUI()
    {
        goldtextMeshProUGUI.text = ("Gold: " + gold.ToString());
    }

    public void GainGold(int increase)
    {
        //Debug.Log("gold gain?" + increase);
        if (increase > 0)
        {
            gold += increase;
        }
        UpdateGoldUI();
    }

    public bool PurchaseCard(int cost)
    {
        if(cost > gold)
        {
            return false;
        }
        gold -= cost;
        UpdateGoldUI();
        return true;
    }

    public void LoadData(GameData data)
    {
        gold = data.gold;
        UpdateGoldUI();
        health = data.health;
        UpdateHealthUI();
    }

    public void SaveData(ref GameData data)
    {
        data.gold = (int)gold;
        data.health = (int)health;
    }
}

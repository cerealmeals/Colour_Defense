using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public GameObject Mana;
    public GameObject Text;
    public float curMana;
    public float maxMana = 100;
    public float tick;
    private float current = 0;
    public float manaScaler = 1;

    private float maxWidth;
    RectTransform rectTransform;
    TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = Text.GetComponent<TextMeshProUGUI>();
        rectTransform = Mana.GetComponent<RectTransform>();
        maxWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2((curMana / maxMana) * maxWidth, rectTransform.rect.height);
        textMeshProUGUI.text = (curMana.ToString() + " / " + maxMana.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (current < tick)
        {
            current = current + Time.deltaTime;
        }
        else
        {
            if(curMana < maxMana)
            {
                curMana = curMana + 1 * manaScaler;
                updateUI();
            }
            current = 0;
        }
    }

    public bool CardPlayable(int cost)
    {
        if (cost <= curMana)
        {
            return true;
        }
        return false;
    }

    private void updateUI()
    {
        rectTransform.sizeDelta = new Vector2((curMana / maxMana) * maxWidth, rectTransform.rect.height);
        textMeshProUGUI.text = (curMana.ToString() + " / " + maxMana.ToString());
    }

    public void PlayCard(int cost)
    {
        if (cost <= curMana)
        {
            curMana -= cost;
            updateUI();
        }
    }

    public void SpeedUpMana(int factor)
    {
        manaScaler += factor;
    }

    public void InstantManaIncrease(int amount)
    {
        curMana =  curMana + amount * manaScaler;
    }
}

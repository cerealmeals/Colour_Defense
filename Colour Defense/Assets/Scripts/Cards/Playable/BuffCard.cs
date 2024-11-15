using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffCard : PlayableCard
{
    public Buff cardData;
    public GameObject BuffPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        handManager = (HandManager)FindAnyObjectByType(typeof(HandManager));
        if (handManager == null)
        {
            Debug.Log("Error: no HandManager found");
        }
        manaManager = (ManaManager)FindAnyObjectByType(typeof(ManaManager));
        if (manaManager == null)
        {
            Debug.Log("Error: no ManaManager found");
        }
        tileManager = (TileManager)FindAnyObjectByType(typeof(TileManager));
        if (tileManager == null)
        {
            Debug.Log("Error: no TileManager found");
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // mousePositionOffset = gameObject.transform.position - GetMNouseWorldPosition();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);

        movingOnDrag = Instantiate(BuffPrefab, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        // movingTower.transform.position = GetMNouseWorldPosition() + mousePositionOffset;

        movingOnDrag.transform.position = GetMNouseWorldPosition() + new Vector3(0, 0, 10);
    }

    private void ResetCard()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        Destroy(movingOnDrag);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // if you have the mana and you placed it on the board
        if (manaManager.CardPlayable(cardData.cardCost) && IsMouseOverTheBoard())
        {

            if (hexCell.towerInCell)
            {
                towerinteraction tower = hexCell.objectInCell.GetComponent<towerinteraction>();
                if (cardData.cardName[0] == 'R')
                {
                    tower.ChangeTowerRange(tower.range + (int)cardData.buffValue);
                }
                else if (cardData.cardName[0] == 'A')
                {
                    tower.ChangeTowerAttackSpeed(cardData.buffValue);
                }
                

                Destroy(movingOnDrag);
                manaManager.PlayCard(cardData.cardCost);
                handManager.RemoveCard(gameObject, cardData);
                Destroy(gameObject);
            }
            else
            {
                ResetCard();
            }
        }
        else
        {
            ResetCard();
        }
    }
}

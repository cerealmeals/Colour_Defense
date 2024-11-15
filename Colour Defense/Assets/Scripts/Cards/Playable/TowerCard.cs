using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard : PlayableCard
{
    public GameObject towerPrefab;
    public GameObject fakeTowerPrefab;

    
    public Tower cardData;

    public float scalingFactor = 10f;

    // private bool cardplayable;
    

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(cardData.red, cardData.green, cardData.blue);
        handManager = (HandManager)FindAnyObjectByType(typeof(HandManager));
        if ( handManager == null )
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
        img.color = new Color(cardData.red, cardData.green, cardData.blue, 0.5f);

        movingOnDrag = Instantiate(fakeTowerPrefab, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
        movingOnDrag.GetComponent<SpriteRenderer>().color = new Color(cardData.red, cardData.green, cardData.blue);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // if you have the mana and you placed it on the board
        if (manaManager.CardPlayable(cardData.cardCost) && IsMouseOverTheBoard())
        {

            // and the cell is not full
            if (!hexCell.full) // placing the tower logic
            {
                GameObject newTower = Instantiate(towerPrefab, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
                newTower.GetComponent<SpriteRenderer>().color = new Color(cardData.red, cardData.green, cardData.blue);
                newTower.GetComponent<towerinteraction>().colours = new Vector3(-(cardData.red / scalingFactor), -(cardData.green / scalingFactor), -(cardData.blue / scalingFactor));
                newTower.GetComponent<towerinteraction>().towerdata = cardData;
                newTower.GetComponent<towerinteraction>().range = (int)cardData.range;
                newTower.GetComponent<towerinteraction>().scalingFactor = scalingFactor;
                tileManager.AddObjectToEmptyGrid(newTower, hexCell);
                tileManager.AddToPathLists(hexCell, newTower.GetComponent<towerinteraction>());
                hexCell.towerInCell = true;
                Destroy(movingOnDrag);
                manaManager.PlayCard(cardData.cardCost);
                handManager.RemoveCard(gameObject, cardData);
                Destroy(gameObject);
            }
            else if (hexCell.towerInCell) // adding to tower logic
            {
                GameObject newTower = Instantiate(towerPrefab, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
                newTower.GetComponent<SpriteRenderer>().color = new Color(cardData.red, cardData.green, cardData.blue);
                newTower.GetComponent<towerinteraction>().colours = new Vector3(-(cardData.red / scalingFactor), -(cardData.green / scalingFactor), -(cardData.blue / scalingFactor));
                newTower.GetComponent<towerinteraction>().towerdata = cardData;

                hexCell.objectInCell.GetComponent<towerinteraction>().AddTowers(newTower.GetComponent<towerinteraction>());
                Destroy(newTower);

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

    public override void OnDrag(PointerEventData eventData)
    {
        // movingTower.transform.position = GetMNouseWorldPosition() + mousePositionOffset;

        movingOnDrag.transform.position = GetMNouseWorldPosition() + new Vector3(0, 0, 10);  
    }

    private void ResetCard()
    {
        img.color = new Color(cardData.red, cardData.green, cardData.blue, 1f);
        Destroy(movingOnDrag);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject tower;
    public GameObject fakeTower;

    private GameObject movingTower;
    public Tower cardData;
    public HandManager handManager;
    public ManaManager manaManager;
    public TileManager tileManager;

    private HexCell hexCell;

    // private bool cardplayable;

    Image img;
    

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

    private Vector3 GetMNouseWorldPosition()
    {
        // capture mouse position and return Worldpoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        // mousePositionOffset = gameObject.transform.position - GetMNouseWorldPosition();
        img.color = new Color(cardData.red, cardData.green, cardData.blue, 0.5f);

        movingTower = Instantiate(fakeTower, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
        movingTower.GetComponent<SpriteRenderer>().color = new Color(cardData.red, cardData.green, cardData.blue);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // if you have the mana and you placed it on the board
        if (manaManager.CardPlayable(cardData.cardCost) && ValidMouseLocation())
        {

            // and the cell is not full
            if (!hexCell.full)
            {
                GameObject newTower = Instantiate(tower, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
                newTower.GetComponent<SpriteRenderer>().color = new Color(cardData.red, cardData.green, cardData.blue);
                newTower.GetComponent<towerinteraction>().colours = new Vector3(-(cardData.red / 10f), -(cardData.green / 10f), -(cardData.blue / 10f));
                newTower.GetComponent<towerinteraction>().towerdata = cardData;
                tileManager.AddObjectToEmptyGrid(newTower, hexCell);
                tileManager.AddToPathLists(hexCell, newTower.GetComponent<towerinteraction>());
                hexCell.towerInCell = true;
                Destroy(movingTower);
                manaManager.PlayCard(cardData.cardCost);
                handManager.RemoveCard(gameObject);
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

    public void OnDrag(PointerEventData eventData)
    {
        // movingTower.transform.position = GetMNouseWorldPosition() + mousePositionOffset;

        movingTower.transform.position = GetMNouseWorldPosition() + new Vector3(0, 0, 10);  
    }

    private void ResetCard()
    {
        img.color = new Color(cardData.red, cardData.green, cardData.blue, 1f);
        Destroy(movingTower);
    }

    private bool ValidMouseLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Cells"));

        if (hit.collider != null && hit.collider.GetComponent<HexCell>())
        {
            hexCell = hit.collider.GetComponent<HexCell>();
            return true;
        }
        return false;
    }
}

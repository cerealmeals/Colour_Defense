using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public Vector2 gridIndex;

    public bool full = false;

    public bool towerInCell;

    public GameObject objectInCell;

    private Color col;

    public TextMeshProUGUI textMeshProUGUI;

    //Vector3 mousePositionOffset;

    //private GameObject movingGameObject;

    public TileManager tileManager;

    void Awake()
    {
        objectInCell = null;
        textMeshProUGUI = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        Color col = gameObject.GetComponent<SpriteRenderer>().color;
        tileManager = (TileManager)FindAnyObjectByType(typeof(TileManager));
        if (tileManager == null)
        {
            Debug.Log("Error: no TileManager found");
        }
    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        gameObject.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 0.5F);
        if (towerInCell )
        {
            List<GameObject> cellsInTowerRange = objectInCell.GetComponent<towerinteraction>().cellsInRange;
            for(int i = 0; i < cellsInTowerRange.Count; i++)
            {
                cellsInTowerRange[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.7F);
            }
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        gameObject.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 0);
        if (towerInCell)
        {
            List<GameObject> cellsInTowerRange = objectInCell.GetComponent<towerinteraction>().cellsInRange;
            for (int i = 0; i < cellsInTowerRange.Count; i++)
            {
                cellsInTowerRange[i].GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 0);
            }
        }
    }

    public void AddIndex(Vector2 vec)
    {
        gridIndex = vec;
        textMeshProUGUI.text = (vec.x.ToString() + " / " + vec.y.ToString());
    }

    //private void OnMouseDown()
    //{
    //    if (towerInCell)
    //    {
    //        mousePositionOffset = objectInCell.transform.position - GetMNouseWorldPosition();
    //        // rangeIndicator.SetActive(true);
    //        movingGameObject = Instantiate(objectInCell, GetMNouseWorldPosition(), Quaternion.identity);
    //        movingGameObject.GetComponent<SpriteRenderer>().color = new Color(objectInCell.GetComponent<SpriteRenderer>().color.r, objectInCell.GetComponent<SpriteRenderer>().color.g, objectInCell.GetComponent<SpriteRenderer>().color.b, 0.6F);
    //    }
    //}

    //private void OnMouseDrag()
    //{
    //    if (towerInCell)
    //    {
    //        movingGameObject.transform.position = GetMNouseWorldPosition() + mousePositionOffset;
    //    }
    //}

    //private void OnMouseUp()
    //{
    //    if (towerInCell)
    //    {
    //        HexCell hexCell = ValidMouseLockation();
    //        if (hexCell != null && hexCell.full == false)
    //        {

    //            tileManager.RemoveFromPathLists(hexCell, objectInCell.GetComponent<towerinteraction>());


    //            // add to the other cell
    //            tileManager.AddObjectToEmptyGrid(objectInCell, hexCell);
    //            tileManager.AddToPathLists(hexCell, objectInCell.GetComponent<towerinteraction>());
    //            hexCell.towerInCell = true;

    //            // reset current cell
    //            full = false;
                
    //            objectInCell = null;
    //            towerInCell = false;
    //        }

    //        // rangeIndicator.SetActive(false);
    //        Destroy(movingGameObject);
    //    }
    //}

    //private Vector3 GetMNouseWorldPosition()
    //{
        
    //    // capture mouse position and return Worldpoint
    //    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}

    //private HexCell ValidMouseLockation()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Cells"));

    //    if (hit.collider != null && hit.collider.GetComponent<HexCell>())
    //    {
    //        return hit.collider.GetComponent<HexCell>();
            
    //    }
    //    return null;
    //}
}

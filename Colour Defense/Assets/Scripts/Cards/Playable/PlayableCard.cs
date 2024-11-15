using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayableCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public HandManager handManager;
    public ManaManager manaManager;
    public TileManager tileManager;

    protected HexCell hexCell;
    protected Image img;
    protected GameObject movingOnDrag;

    public virtual void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    protected Vector3 GetMNouseWorldPosition()
    {
        // capture mouse position and return Worldpoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected bool IsMouseOverTheBoard()
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

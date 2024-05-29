using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAble : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public GameObject rangeIndicator;
    public GameObject fakeTower;

    private GameObject movingTower;
    SpriteRenderer m_SpriteRenderer;
    public float red = 0;
    public float green = 0;
    public float blue = 0;

    private Vector3 GetMNouseWorldPosition()
    {
        // capture mouse position and return Worldpoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePositionOffset = gameObject.transform.position - GetMNouseWorldPosition();
        rangeIndicator.SetActive(true);
        movingTower = Instantiate(fakeTower, GetMNouseWorldPosition(), Quaternion.identity);
        movingTower.GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
    }

    private void OnMouseDrag()
    {
        movingTower.transform.position = GetMNouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUp()
    {
        transform.position = GetMNouseWorldPosition() + mousePositionOffset;
        rangeIndicator.SetActive(false);
        Destroy(movingTower);
        int layerObject = 7;
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray, layerObject);
        if (hit.collider != null)
        {
            Debug.Log("you hit this thing");
            Debug.Log(hit.collider.gameObject.GetComponent<SpriteRenderer>().color);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        red = m_SpriteRenderer.color.r;
        green = m_SpriteRenderer.color.g;
        blue = m_SpriteRenderer.color.b;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}

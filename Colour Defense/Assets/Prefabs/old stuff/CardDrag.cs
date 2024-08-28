using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : MonoBehaviour
{

    //Vector3 mousePositionOffset;

    
    public GameObject tower;
    public GameObject fakeTower;

    private GameObject movingTower;

    SpriteRenderer m_SpriteRenderer;
    public float red = 0;
    public float green = 0;
    public float blue = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        red = m_SpriteRenderer.color.r;
        green = m_SpriteRenderer.color.g;
        blue = m_SpriteRenderer.color.b;
    }

    private Vector3 GetMNouseWorldPosition()
    {
        // capture mouse position and return Worldpoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        // mousePositionOffset = gameObject.transform.position - GetMNouseWorldPosition();
        m_SpriteRenderer.color = new Color(red, green, blue, 0.5f);
        
        movingTower = Instantiate(fakeTower, GetMNouseWorldPosition() + new Vector3(0,0,10), Quaternion.identity);
        movingTower.GetComponent<SpriteRenderer>().color = new Color(red, green, blue);

    }

    private void OnMouseDrag()
    {
        // movingTower.transform.position = GetMNouseWorldPosition() + mousePositionOffset;

        movingTower.transform.position = GetMNouseWorldPosition() + new Vector3(0, 0, 10);
    }

    private void OnMouseUp()
    {
        // transform.position = GetMNouseWorldPosition() + mousePositionOffset;
        
        GameObject newTower = Instantiate(tower, GetMNouseWorldPosition() + new Vector3(0, 0, 10), Quaternion.identity);
        newTower.GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
        newTower.GetComponent<towerinteraction>().colours = new Vector3(-(red / 10f), -(green / 10f), -(blue / 10f));
        Destroy(movingTower);
        Destroy(gameObject);
    }

    

    //// Update is called once per frame
    //void Update()
    //{

    //}
}

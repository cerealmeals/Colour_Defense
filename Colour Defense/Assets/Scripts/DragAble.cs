using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAble : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public GameObject rangeIndicator;
    public GameObject fakeTower;

    private GameObject movingTower;

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
    }
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}

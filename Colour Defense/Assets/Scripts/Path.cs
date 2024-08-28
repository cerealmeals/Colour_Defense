using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;

public class Path : MonoBehaviour
{

    public List<Transform> sortedPointsTransform;

    public float movespeed;

    private int i = 0;

    private void FindMapWaypoints()
    {
        GameObject[] WayPointsObjects = GameObject.FindGameObjectsWithTag("Points");
        foreach (GameObject child in WayPointsObjects)
        {
            sortedPointsTransform.Add(child.transform);
        }
        sortedPointsTransform = sortedPointsTransform.OrderBy(go => go.name).ToList();

    }

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 2;
        FindMapWaypoints();
        transform.position = sortedPointsTransform[i].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, sortedPointsTransform[i].transform.position, movespeed * Time.deltaTime);

        if(transform.position == sortedPointsTransform[i].position)
        {
            i++;
        }

        if(i > sortedPointsTransform.Count - 1)
        {
            Destroy(gameObject);
        }
    }
    
    // for sorting the list of points
    //private static int SortByName(GameObject o1, GameObject o2)
    //{
    //    return o1.name.CompareTo(o2.name);
    //}
}

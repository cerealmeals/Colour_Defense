using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    Transform[] Points;

    public float movespeed;

    private int i = 0;

    private void FindMapWaypoints()
    {
        GameObject waypointContainer = GameObject.FindGameObjectWithTag("Points");
        List<Transform> waypointList = new List<Transform>();
        foreach (Transform child in waypointContainer.transform)
        {
            waypointList.Add(child);
        }
        Points = waypointList.ToArray();

    }

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 2;
        FindMapWaypoints();
        transform.position = Points[i].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Points[i].transform.position, movespeed * Time.deltaTime);

        if(transform.position == Points[i].transform.position)
        {
            i++;
        }

        if(i > Points.Length - 1)
        {
            Destroy(gameObject);
        }
    }
}

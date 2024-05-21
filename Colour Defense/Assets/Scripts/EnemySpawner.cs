using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float tick = 1;
    public float current = 0;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (current < tick)
        {
            current = current + Time.deltaTime;
        }
        else
        {
            spawnEnemy();
            current = 0;
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemy);
    }
}

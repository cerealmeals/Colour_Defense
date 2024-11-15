using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float tick = 1;
    public float current = 0;
    public GameObject enemy;
    GoldAndHealthManager goldAndHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy();
        goldAndHealthManager = (GoldAndHealthManager)FindAnyObjectByType(typeof(GoldAndHealthManager));
        if (goldAndHealthManager == null)
        {
            Debug.Log("Error: no GoldAndHealthManager found");
        }
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
        GameObject current = Instantiate(enemy, gameObject.transform);
        Rigidbody2D body = current.GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        body.useFullKinematicContacts = true;
        EnemyBehaviour enemydata = current.GetComponent<EnemyBehaviour>();
        enemydata.damageScale = 1;
        enemydata.playerdamage = 1;
        enemydata.gold = 10;
        enemydata.goldAndHealthManager = goldAndHealthManager;
    }
}

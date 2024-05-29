using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class towerinteraction : MonoBehaviour
{
    public float tick = 1;
    public float current = 0;
    public Vector3 colours;

    public List<EnemyBehaviour> enemiesInRange;

    // Start is called before the first frame update
    //public GameObject[] enemies;
    void Start()
    {
        //if (enemies == null)
        //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRange.Count != 0)
        {
            if (current < tick)
            {
                current = current + Time.deltaTime;
            }
            else
            {
                foreach (EnemyBehaviour enemy in enemiesInRange)
                {
                    enemy.TakeDamage(colours);
                    //enemy.SendMessage("TakeDamage", new Vector3(red, green, blue));
                }
                current = 0;
            }
        }
        else
        {
            current = current + Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.GetComponent<EnemyBehaviour>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.GetComponent<EnemyBehaviour>());
        }
    }
}

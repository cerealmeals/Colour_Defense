using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public List<towerinteraction> towers;
    public List<EnemyBehaviour> enemiesInCurrentPath;
    private GameObject cell;


    private void Start()
    {
        cell = this.gameObject.transform.parent.gameObject;
    }
    public void AddTower(towerinteraction tower)
    {
        towers.Add(tower);
        foreach (EnemyBehaviour enemy in enemiesInCurrentPath)
        {
            tower.AddEnemyToList(enemy);
        }
    }

    public void RemoveTower(towerinteraction tower)
    {
        towers.Remove(tower);
        foreach (EnemyBehaviour enemy in enemiesInCurrentPath)
        {
            tower.RemoveEnemyFromList(enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.name);
        if (collision.CompareTag("Enemy"))
        {
            enemiesInCurrentPath.Add(collision.GetComponent<EnemyBehaviour>());
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].AddEnemyToList(collision.GetComponent<EnemyBehaviour>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInCurrentPath.Remove(collision.GetComponent<EnemyBehaviour>());
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].RemoveEnemyFromList(collision.GetComponent<EnemyBehaviour>());
            }
        }
    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message

        
        cell.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5F);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        cell.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}

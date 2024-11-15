using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class towerinteraction : MonoBehaviour
{
    public float tick;
    public float current = 0;
    public Vector3 colours;
    public Tower towerdata;
    public int range = 2;

    public float scalingFactor;

    public List<EnemyBehaviour> enemiesInRange;

    public List<GameObject> cellsInRange;

    // Start is called before the first frame update
    //public GameObject[] enemies;
    void Start()
    {
        tick = towerdata.attackSpeed;
        range = (int)towerdata.range;
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
                List<EnemyBehaviour> distinctEnemies = enemiesInRange.Distinct().ToList();
                foreach (EnemyBehaviour enemy in distinctEnemies)
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

    public void AddEnemyToList(EnemyBehaviour enemy)
    {
        enemiesInRange.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyBehaviour enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    public void AddTowers(towerinteraction tower)
    {
        // Debug.Log("AddTower");
        colours += tower.colours;
        Vector3 normal = -colours * scalingFactor;
        normal = DivideByLargestFactor(normal);
        //  Debug.Log(normal);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(normal.x , normal.y, normal.z);
        if(tick < tower.tick)
        {
            tick = tower.tick;
            
        }
        if(range < tower.range)
        {
            ChangeTowerRange(tower.range);
        }
    }

    private Vector3 DivideByLargestFactor(Vector3 vec)
    {
        float max = -1f;
        int index = 0;

        for (int i = 0; i < 3;  i++)
        {
            if (vec[i] > max)
            {
                index = i;
                max = vec[0];
            }
        }
        //Debug.Log(max);
        //Debug.Log(index);
        return new Vector3(vec[0] / vec[index], vec[1] / vec[index], vec[2] / vec[index]);
    }

    public void ChangeTowerRange(int range)
    {
        HexCell hexCell = this.gameObject.transform.parent.GetComponent<HexCell>();
        hexCell.tileManager.RemoveFromPathLists(hexCell, this);

        this.range = range;

        hexCell.tileManager.AddToPathLists(hexCell, this);
    }

    public void ChangeTowerAttackSpeed(float factor)
    {
        tick *= factor;
    }
}

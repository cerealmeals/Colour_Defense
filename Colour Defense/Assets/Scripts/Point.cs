using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public List<towerinteraction> towers;

    public void addTower(towerinteraction tower)
    {
        towers.Add(tower);
    }

    public void removeTower(towerinteraction tower)
    {
        towers.Remove(tower);
    }
}

using Cerealmeals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int gold;
    public int health;
    public List<Card> deck;
    public int level;

    public GameData()
    {
        this.gold = 0;
        this.health = 100;
        this.deck = new List<Card>();
        this.level = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CardDealer : MonoBehaviour
{

    public GameObject card;
    public float tick = 4;
    public float current = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject red = Instantiate(card, transform.position, transform.rotation);
        red.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 0, 0);
        GameObject green = Instantiate(card, transform.position, transform.rotation);
        green.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0, 1, 0);
        GameObject blue = Instantiate(card, transform.position, transform.rotation);
        blue.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0, 0, 1);
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
            GameObject nextcard = Instantiate(card, transform.position, Quaternion.identity);
            nextcard.GetComponent<SpriteRenderer>().color = PickAColour();
            current = 0;
        }
    }

    private UnityEngine.Color PickAColour()
    {
        int pick = Random.Range(1,4);
        Debug.Log(pick);
        switch (pick)
        {
            case 1:
                return new UnityEngine.Color(1,0,0);
            case 2:
                return new UnityEngine.Color(0,1,0);
            case 3:
                return new UnityEngine.Color(0,0,1);
            default:
                return new UnityEngine.Color(0, 0, 0);
        }
    }
}

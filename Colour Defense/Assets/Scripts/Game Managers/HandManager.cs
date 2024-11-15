using Cerealmeals;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class HandManager : MonoBehaviour
{
    public GameObject towerCardPrefab;

    public GameObject buffCardPrefab;

    public GameObject miscCardPrefab;

    public GameObject cardHolder;

    public DeckManager deckManager;
    
    public List<GameObject> cardsInHand = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        deckManager = FindAnyObjectByType<DeckManager>();
        if (deckManager == null)
        {
            Debug.Log("Error DeckManager not found");
        }

        // fill hand with 4 cards
        deckManager.DrawStaringHand(this);
    }

    public void AddCardtoHand(Card card)
    {
        // make a new card object
        GameObject newCard;
        // add the card data to the list of cards in hand

        // check card type and do special things
        if (card.GetType() == typeof(Tower))
        {
            newCard = Instantiate(towerCardPrefab);
            newCard.GetComponent<TowerCard>().cardData = (Tower)card;
            
        }
        else if(card.GetType() == typeof(Buff))
        {
            newCard = Instantiate(buffCardPrefab);
            newCard.GetComponent<BuffCard>().cardData = (Buff)card;          
        }
        else
        {
            newCard = Instantiate(miscCardPrefab);
            //Debug.Log(newCard);
            newCard.GetComponent<MiscCard>().cardData = (Misc)card;
        }

        SetUpNameDescriptionCost(newCard, card);
        cardsInHand.Add(newCard);
        // put the new card as a child of the card holder
        newCard.transform.SetParent(cardHolder.transform, false);
    }


    private void SetUpNameDescriptionCost(GameObject card, Card cardData)
    {
        GameObject changeName = card.transform.Find("Name").gameObject;
        changeName.GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        GameObject changeDescription = card.transform.Find("Description").gameObject;
        changeDescription.GetComponent<TextMeshProUGUI>().text = cardData.cardDescription;
        GameObject changeCost = card.transform.Find("Cost").gameObject;
        changeCost.GetComponent<TextMeshProUGUI>().text = cardData.cardCost.ToString();
    }

    public void RemoveCard(GameObject card, Card cardData)
    {
        cardsInHand.Remove(card);
        deckManager.CardPlayed(cardData);
        deckManager.DrawCard(this);
    }

   
}

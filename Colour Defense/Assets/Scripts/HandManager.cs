using Cerealmeals;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;

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
        deckManager.DrawCard(this);
        deckManager.DrawCard(this);
        deckManager.DrawCard(this);
        deckManager.DrawCard(this);
    }

    public void AddCardtoHand(Card card)
    {
        // make a new card object
        GameObject newCard = Instantiate(cardPrefab);
        // add the card data to the list of cards in hand
        cardsInHand.Add(newCard);

        // check card type and do special things
        if (card.GetType() == typeof(Tower))
        {
            newCard.GetComponent<TowerCard>().cardData = (Tower)card;
            SetUpTowerCard(newCard, newCard.GetComponent<TowerCard>().cardData);
        }

        // put the new card as a child of the card holder
        newCard.transform.SetParent(cardHolder.transform, false);
    }

    private void SetUpTowerCard(GameObject card, Tower cardData)
    {
        GameObject changeName = card.transform.Find("Name").gameObject;
        changeName.GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        GameObject changeDescription = card.transform.Find("Description").gameObject;
        changeDescription.GetComponent<TextMeshProUGUI>().text = cardData.cardDescription;
        GameObject changeCost = card.transform.Find("Cost").gameObject;
        changeCost.GetComponent<TextMeshProUGUI>().text = cardData.cardCost.ToString();
    }

    public void RemoveCard(GameObject card)
    {
        cardsInHand.Remove(card);
        deckManager.DrawCard(this);
    }

   
}

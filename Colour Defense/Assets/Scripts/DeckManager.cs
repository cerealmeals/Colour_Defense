using Cerealmeals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public List<Card> deck;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Load all card assest from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");
        //add the loaded cards to the allCards list
        allCards.AddRange(cards);
        ShuffleDeck(allCards);
    }

    public void DrawCard(HandManager handManager)
    {
        // Debug.Log("Check: Card count" + allCards.Count);
        // if there are no cards return nothing
        if (allCards.Count == 0)
        {
            Debug.Log("Card count is zero");
            return;
        }
        Card nextCard = allCards[currentIndex];
        handManager.AddCardtoHand(nextCard);
        currentIndex = (currentIndex + 1) % allCards.Count;
        if (currentIndex == 0 )
        {
            ShuffleDeck(allCards);
        }
    }

    public void ShuffleDeck(List<Card> deck)
    {
        for (int t = 0; t < deck.Count; t++)
        {
            Card tmp = deck[t];
            int r = Random.Range(t, deck.Count);
            deck[t] = deck[r];
            deck[r] = tmp;
        }
    }
}

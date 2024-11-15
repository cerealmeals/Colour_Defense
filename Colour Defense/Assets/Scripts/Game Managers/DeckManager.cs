using Cerealmeals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour, IDatapersistence
{
    public List<Card> allCards = new List<Card>();
    public List<Card> deck = new List<Card>();
    private List<Card> cardsInHand = new List<Card>();
    private List<Card> discardPile = new List<Card>();

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start deck");
        // Load all card assest from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");
        //add the loaded cards to the allCards list
        allCards.AddRange(cards);

        if (deck.Count == 0)
        {
            Debug.Log("check");
            deck = allCards;
        }
        
        ShuffleDeck(deck);
    }

    public void DrawCard(HandManager handManager)
    {
        // Debug.Log("Check: Card count" + allCards.Count);
        // if there are no cards return nothing
        if (deck.Count == 0)
        {
            Debug.Log("Card count is zero");
            return;
        }
        Card nextCard = deck[0];
        handManager.AddCardtoHand(nextCard);
        cardsInHand.Add(nextCard);
        deck.Remove(nextCard);
        if (deck.Count == 0)
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
            ShuffleDeck(deck);
        }
        //currentIndex = (currentIndex + 1) % deck.Count;
        //if (currentIndex == 0 )
        //{
            
        //}
    }

    public void ShuffleDeck(List<Card> currentdeck)
    {
        for (int t = 0; t < currentdeck.Count; t++)
        {
            Card tmp = currentdeck[t];
            int r = Random.Range(t, currentdeck.Count);
            currentdeck[t] = currentdeck[r];
            currentdeck[r] = tmp;
        }
    }

    public void DrawStaringHand(HandManager handManager)
    {
        if (deck.Count == 0)
        {
            Debug.Log("Card count is zero");
            return;
        }
        for (int i = 0;i < deck.Count;)
        {
            Card card = deck[i];
            switch (card.cardName)
            {
                case "Red Tower":
                    StartingHandDraw(handManager, card);
                    break;
                case "Blue Tower":
                    StartingHandDraw(handManager, card);
                    break;
                case "Green Tower":
                    StartingHandDraw(handManager, card);
                    break;
                default:
                    i++;
                    break;
            }
        }
    }

    private void StartingHandDraw(HandManager handManager, Card card)
    {
        handManager.AddCardtoHand(card);
        cardsInHand.Add(card);
        deck.Remove(card);
    }

    public void CardPlayed(Card card)
    {
        cardsInHand.Remove(card);
        discardPile.Add(card);
    }

    public void LoadData(GameData data)
    {
        Debug.Log("load deck");
        deck = data.deck;
    }

    public void SaveData(ref GameData data)
    {
        foreach(Card card in cardsInHand)
        {
            deck.Add(card);
        }
        foreach(Card card in discardPile)
        {
            deck.Add(card);
        }
        data.deck = deck;
    }
}

using System.Collections;
using System.Collections.Generic;

public class Hand
{
    private List<Card> hand;

    public Hand()
    {
        hand = new List<Card>();
    }

    public void AddToHand(Card card)
    {
        hand.Add(card);
    }

    public void ClearHand()
    {
        hand.Clear();
    }

    public List<Card> GetHand()
    {
        return hand;
    }

    public Card GetTopCard()
    {
        return hand[hand.Count - 1];
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<Card> originalList;
    public Stack<Card> shuffledCards;

    public Deck()
    {
        originalList = new List<Card>();
        shuffledCards = new Stack<Card>();
        BuildNewDeck();
        ShuffleCards();
    }

    public void BuildNewDeck()
    {
        originalList.Clear();
        Suit[] suits = new Suit[]{Suit.Club, Suit.Diamond, Suit.Heart, Suit.Spade};
        foreach(Suit suit in suits)
        {
            for (int i = 1; i < 14; i++)
            {
                if (i == 1)
                {
                    originalList.Add(new Card(i, suit, Face.Ace));
                }
                else if (i < 11)
                {
                    originalList.Add(new Card(i, suit, Face.Numerical));
                }
                else if (i == 11)
                {
                    originalList.Add(new Card(10, suit, Face.Jack));
                }
                else if (i == 12)
                {
                    originalList.Add(new Card(10, suit, Face.Queen));
                }
                else if (i == 13)
                {
                    originalList.Add(new Card(10, suit, Face.King));
                }
            }
        }
        Debug.Log("Size of deck: " + originalList.Count);
    }

    public void ShuffleCards()
    {
        shuffledCards.Clear();
        while (originalList.Count != 0)
        {
            int index = Random.Range(0, originalList.Count);
            shuffledCards.Push(originalList[index]);
            originalList.RemoveAt(index);
        }
    }

    public Card Draw()
    {
        if (shuffledCards.Count > 0)
        {
            return shuffledCards.Pop();
        }
        return null;
    }

    public int GetDeckSize()
    {
        return shuffledCards.Count;
    }
}
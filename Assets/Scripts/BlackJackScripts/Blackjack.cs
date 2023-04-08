using System.Collections;
using System.Collections.Generic;


public class Blackjack {
    
    private Hand hand;
    private Deck deck;
    private int handValue;
    private bool waitingOnAce;

    public Blackjack()
    {
        handValue = 0;
        hand = new Hand();
        deck = new Deck();
    }

/*     private void Update()
    {
        if (waitingOnAce)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CountAceLow();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                CountAceHigh();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            HitMe();
        }
    } */

    public void CountAceHigh()
    {
        handValue += 11;
        waitingOnAce = false;
        // Need to check if busted
    }

    public void CountAceLow()
    {
        handValue += 1;
        waitingOnAce = false;
        // Need to check if busted
    }

    public int GetValue()
    {
        return handValue;
    }

    public bool IsBusted()
    {
        return handValue > 21;
    }

    public void HitMe()
    {
        if (IsBusted())
        {
            ResetHand();
        }
        if (waitingOnAce) return;
        if (deck.GetDeckSize() == 0)
        {
            deck.BuildNewDeck();
            deck.ShuffleCards();
        }


        Card newCard = deck.Draw();
        hand.AddToHand(newCard);
        if (newCard.GetFace() == Face.Ace)
        {
            waitingOnAce = true;
            return;
        }
        else
        {
            handValue += newCard.GetValue();
        }
    }

    public bool WaitingOnAce()
    {
        return waitingOnAce;
    }

    public Hand GetHand()
    {
        return hand;
    }

    public Card GetTopHandCard()
    {
        return hand.GetTopCard();
    }

    public void ResetHand()
    {
        hand.ClearHand();
        handValue = 0;
        waitingOnAce = false;
    }
}

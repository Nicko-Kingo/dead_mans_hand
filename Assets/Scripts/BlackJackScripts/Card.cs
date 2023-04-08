using System.Collections;
using System.Collections.Generic;

public class Card
{
    private int value;
    private Suit suit;
    private Face face;

    public Card(int value, Suit suit, Face face)
    {
        this.value = value;
        this.suit = suit;
        this.face = face;
    }

    public int GetValue()
    {
        return value;
    }

    public Suit GetSuit()
    {
        return suit;
    }

    public Face GetFace()
    {
        return face;
    }

    public override string ToString()
    {
        string retStr = "";
        switch (face)
        {
            case Face.Numerical:
                retStr += value;
            break;
            case Face.Jack:
                retStr += "Jack";
            break;
            case Face.Queen:
                retStr += "Queen";
            break;
            case Face.King:
                retStr += "King";
            break;
            case Face.Ace:
                retStr += "Ace";
            break;
        }
        retStr += " of ";
        switch (suit)
        {
            case Suit.Spade:
                retStr += "Spades";
            break;
            case Suit.Club:
                retStr += "Clubs";
            break;
            case Suit.Diamond:
                retStr += "Diamonds";
            break;
            case Suit.Heart:
                retStr += "Hearts";
            break;
        }
        return retStr;
    }
}

public enum Suit
{
    Spade,
    Club,
    Diamond,
    Heart
}

public enum Face
{
    Numerical,
    Jack,
    Queen,
    King,
    Ace
}
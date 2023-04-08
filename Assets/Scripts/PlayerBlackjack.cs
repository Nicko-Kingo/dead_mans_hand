using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackjack : MonoBehaviour
{
    private Blackjack blackjack;

    private bool mouseDown = false;
    private bool fDown = false;

    // Start is called before the first frame update
    void Start()
    {
        blackjack = new Blackjack();
        StartCoroutine(BlackjackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            fDown = true;
        }
    }

    private IEnumerator BlackjackRoutine()
    {
        while (true)
        {
            if (mouseDown)
            {
                mouseDown = false;
                blackjack.ResetHand();
            }
            else if (fDown)
            {
                fDown = false;
                blackjack.HitMe();
                Debug.Log("You drew the " + blackjack.GetTopHandCard());
                if (blackjack.WaitingOnAce())
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        blackjack.CountAceLow();
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        blackjack.CountAceHigh();
                    }
                }
                Debug.Log("Hand: " + blackjack.GetValue());
                if (blackjack.IsBusted())
                {
                    Debug.Log("You Busted!");
                }
                else if (blackjack.GetValue() == 21)
                {
                    Debug.Log("Blackjack!");
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

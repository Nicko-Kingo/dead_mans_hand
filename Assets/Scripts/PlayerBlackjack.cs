using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBlackjack : MonoBehaviour
{

    private Blackjack blackjack;

    public Camera Cam;

    private bool mouseDown = false;
    private bool fDown = false;

    public TextMeshProUGUI handValueText;
    public Transform cardHolder;
    public GameObject cardPrefab;
    public List<GameObject> uiCards;
    public GameObject aceChoice;
    public float cardSpacing = 10f;
    public float stunTime = 2f;

    //public bool stunned = false;

    // Start is called before the first frame update
    void Start()
    {
        blackjack = new Blackjack();
        uiCards = new List<GameObject>();
        StartCoroutine(BlackjackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (!blackjack.WaitingOnAce() && Input.GetKeyDown(KeyCode.F))
        {
            fDown = true;
        }
    }

    private void ShowCards()
    {
        Card topCard = blackjack.GetTopHandCard();
        GameObject newCard = Instantiate(cardPrefab);
        float position = ((uiCards.Count - 1) * cardSpacing) / 2;
        newCard.transform.SetParent(cardHolder);
        newCard.transform.localPosition = new Vector3(position, -100, 0);
        newCard.transform.localScale = Vector3.one;

        string cardMoniker = "";
        switch (topCard.GetFace())
        {
            case Face.Numerical:
                cardMoniker += topCard.GetValue();
            break;
            case Face.Ace:
                cardMoniker += "A";
            break;
            case Face.Jack:
                cardMoniker += "J";
            break;
            case Face.Queen:
                cardMoniker += "Q";
            break;
            case Face.King:
                cardMoniker += "K";
            break;
        }

        newCard.GetComponentInChildren<TextMeshProUGUI>().text = cardMoniker;
        uiCards.Add(newCard);
        handValueText.text = blackjack.GetValue().ToString();
        SetCardPositions();
    }

    private void SetCardPositions()
    {
        Vector3 newPosition = Vector3.zero;
        newPosition.x = -((uiCards.Count - 1) * cardSpacing) / 2;
        for (int i = 0; i < uiCards.Count; i++)
        {
            LeanTween.cancel(uiCards[i]);
            LeanTween.moveLocal(uiCards[i], newPosition, .5f).setEaseInOutSine();
            newPosition.x += cardSpacing;
        }
    }

    private void ResetVisibleCards()
    {
        for (int i = 0; i < uiCards.Count; i++)
        {
            LeanTween.cancel(uiCards[i]);
            Destroy(uiCards[i]);
        }
        uiCards.Clear();
        handValueText.text = "0";
        
    }

    private void ShowAceChoice()
    {
        aceChoice.SetActive(true);
    }

    private void HideAceChoice()
    {
        aceChoice.SetActive(false);
    }

    private IEnumerator BlackjackRoutine()
    {
        while (true)
        {
            if (mouseDown)
            {
                mouseDown = false;

                GetComponent<Attacks>().Attack(blackjack.GetValue(), Cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,15));

                blackjack.ResetHand();
                ResetVisibleCards();
            }
            else if (fDown)
            {
                fDown = false;
                blackjack.HitMe();
                ShowCards();
                Debug.Log("You drew the " + blackjack.GetTopHandCard());
                if (blackjack.WaitingOnAce())
                {
                    ShowAceChoice();
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
                    HideAceChoice();
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        blackjack.CountAceLow();
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        blackjack.CountAceHigh();
                    }
                    handValueText.text = blackjack.GetValue().ToString();
                }
                Debug.Log("Hand: " + blackjack.GetValue());
                if (blackjack.IsBusted())
                {
                    Debug.Log("You Busted!");
                    yield return new WaitForSeconds(stunTime);
                    ResetVisibleCards();
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

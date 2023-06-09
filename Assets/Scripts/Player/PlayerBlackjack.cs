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
    public float cardCoolTime = 1f;

    private PlayerUIController pui;

    private AudioSource playerAudio;

    public AudioClip[] clips;

    //public bool stunned = false;

    // Start is called before the first frame update
    void Start()
    {
        blackjack = new Blackjack();
        uiCards = new List<GameObject>();
        pui = GetComponent<PlayerUIController>();
        playerAudio = GetComponent<AudioSource>();
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
        pui.UpdateGauge(blackjack.GetValue());
    }

    private void SetCardPositions()
    {
        Vector3 newPosition = Vector3.zero;
        Vector3 newRot = Vector3.zero;
        newPosition.x = -((uiCards.Count - 1) * cardSpacing) / 2;
        for (int i = 0; i < uiCards.Count; i++)
        {
            newRot.z = Random.Range(-30f, 30f);
            LeanTween.cancel(uiCards[i]);
            LeanTween.moveLocal(uiCards[i], newPosition, .5f).setEaseInOutSine();
            if (i == uiCards.Count - 1)
            {
                LeanTween.rotate(uiCards[i], newRot, .5f);
            }
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
        pui.UpdateGauge(0);
        
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

                GetComponent<Attacks>().Attack
                (blackjack.GetValue(), Cam.ScreenToWorldPoint(Input.mousePosition), "Player", blackjack.IsTrueBlackjack());

                blackjack.ResetHand();
                ResetVisibleCards();
            }
            else if (fDown)
            {
                fDown = false;
                blackjack.HitMe();
                ShowCards();
                playerAudio.PlayOneShot(clips[0]); // Draw card sfx
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
                    pui.UpdateGauge(blackjack.GetValue());
                }
                Debug.Log("Hand: " + blackjack.GetValue());
                if (blackjack.IsBusted())
                {
                    Debug.Log("You Busted!");
                    playerAudio.PlayOneShot(clips[1]); //Bust sfx
                    pui.SetCoolDown(stunTime);
                    yield return new WaitForSeconds(stunTime);
                    ResetVisibleCards();
                }
                else if (blackjack.GetValue() == 21)
                {
                    playerAudio.PlayOneShot(clips[2]); //Blackjack sfx
                    Debug.Log("Blackjack!");
                }
                else
                {
                    pui.SetCoolDown(cardCoolTime);
                    yield return new WaitForSeconds(cardCoolTime);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

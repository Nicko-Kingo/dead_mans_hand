using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackjack : MonoBehaviour
{
    private Blackjack blackjack;

    public float attackChance = .5f;

    public float stunTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        blackjack = new Blackjack();
        StartCoroutine(BlackjackRoutine());
    }


    private IEnumerator BlackjackRoutine()
    {
        while (true)
        {
            bool attack = Random.Range(0f, 1f) < attackChance && !(blackjack.GetValue() == 0);
            if ((attack || blackjack.GetValue() == 21) && !blackjack.IsBusted())
            {
                EnemyController ec = GetComponent<EnemyController>();
                ec.TempAttack(blackjack.GetValue(), Vector2.zero);

                GameObject player = GameObject.Find("PlayerPrefab");

                GetComponent<Attacks>().Attack
                (blackjack.GetValue(), new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(0,0,15), "Enemy", blackjack.IsTrueBlackjack());

                yield return new WaitUntil(() => ec.PlayAgain());
                blackjack.ResetHand();
                //ResetVisibleCards();
                //Debug.Log("Enemy Attacked!");
            }
            else
            {
                //fDown = false;
                blackjack.HitMe();
                //ShowCards();
                Debug.Log("Enemy drew the " + blackjack.GetTopHandCard());
                if (blackjack.WaitingOnAce())
                {
                    //ShowAceChoice();
                    //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
                    //HideAceChoice();
                    if (blackjack.GetValue() + 11 > 21)
                    {
                        blackjack.CountAceLow();
                    }
                    else
                    {
                        blackjack.CountAceHigh();
                    }
                    //handValueText.text = blackjack.GetValue().ToString();
                }
                Debug.Log("Enemy Hand: " + blackjack.GetValue());
                if (blackjack.IsBusted())
                {
                    Debug.Log("Enemy Busted!");
                    yield return new WaitForSeconds(stunTime);
                    //ResetVisibleCards();
                }
                else if (blackjack.GetValue() == 21)
                {
                    Debug.Log("Enemy Blackjack!");
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}

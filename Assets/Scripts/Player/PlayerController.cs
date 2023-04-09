using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerAnimationController pac;
    private PlayerBlackjack pb;
    private PlayerMovement pm;

    private void Start()
    {
        pac = GetComponent<PlayerAnimationController>();
        pb = GetComponent<PlayerBlackjack>();
        pm = GetComponent<PlayerMovement>();
    }

    public void GotHurt()
    {
        pac.PlayGetHurt();
    }

    public void GameOver()
    {
        pac.PlayDeath();
        pb.enabled = false;
        pm.enabled = false;
        Debug.Log("Game Over!");
    }
}

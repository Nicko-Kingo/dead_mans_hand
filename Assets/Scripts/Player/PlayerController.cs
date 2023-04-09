using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private PlayerAnimationController pac;
    private PlayerBlackjack pb;
    private PlayerMovement pm;
    private PlayerUIController pui;
    public AudioClip hurtClip;

    public static event Action OnGameOver;

    private void Start()
    {
        pac = GetComponent<PlayerAnimationController>();
        pb = GetComponent<PlayerBlackjack>();
        pm = GetComponent<PlayerMovement>();
        pui = GetComponent<PlayerUIController>();
    }

    public void GotHurt(float newHealth)
    {
        pac.PlayGetHurt();
        pui.UpdateHealth(newHealth);
        GetComponent<AudioSource>().PlayOneShot(hurtClip);
    }

    public void GameOver()
    {
        pac.PlayDeath();
        pui.UpdateHealth(0);
        pb.enabled = false;
        pm.enabled = false;
        Debug.Log("Game Over!");
        OnGameOver?.Invoke();
        GameObject.Find("Music").SetActive(false);
    }
}

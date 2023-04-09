using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDeathOptions : MonoBehaviour
{

    public GameObject deathScreen;

    private void Awake()
    {
        PlayerController.OnGameOver += OnPlayerDeath;
    }

    public void OnPlayerDeath()
    {
        PlayerController.OnGameOver -= OnPlayerDeath;
        deathScreen.SetActive(true);
    }
}

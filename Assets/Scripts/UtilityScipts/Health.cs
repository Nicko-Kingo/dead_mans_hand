using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 10f;

    private PlayerController pc;
    private EnemyController ec;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (tag.Equals("Player"))
        {
            pc = GetComponent<PlayerController>();
        }
        else if (tag.Equals("Enemy"))
        {
            ec = GetComponent<EnemyController>();
        }
    }

    // Update is called once per frame
/*     void Update()
    {
        
    } */


    public void TakeDamage(float damage)
    {
        if (health == 0f) return;
        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            if (tag.Equals("Player"))
            {
                pc.GameOver();
            }
            else if (tag.Equals("Enemy"))
            {
                ec.StartDeath();
            }
        }
        else
        {
            if (tag.Equals("Player"))
            {
                pc.GotHurt(health);
            }
            else
            {
                ec.GotHurt();
            }
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

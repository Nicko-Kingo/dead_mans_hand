using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BasicAttack
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Player")) && !owner.Equals(col.gameObject.tag))
        {
            Debug.Log("Hit");
            col.gameObject.GetComponent<Health>().TakeDamage(this.damage);
        }
    }
}

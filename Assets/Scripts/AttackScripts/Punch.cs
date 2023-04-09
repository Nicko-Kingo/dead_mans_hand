using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : BasicAttack
{
/*     private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Bruh");
        if ((other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player")) && !owner.Equals(other.gameObject.tag))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(this.damage);
        }
    } */

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Bruh");
        if ((other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player")) && !owner.Equals(other.gameObject.tag))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(this.damage);
        }
    }
}

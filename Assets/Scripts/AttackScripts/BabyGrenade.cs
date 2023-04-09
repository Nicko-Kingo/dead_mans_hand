using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGrenade : BasicAttack
{
    


    public void explode()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.gameObject.transform.position,1, new Vector3(0,0,0)); //Should only detect enemies
        
        foreach(RaycastHit2D hit in hits)
        {
            if((hit.collider.gameObject.tag.Equals("Enemy") || hit.collider.gameObject.tag.Equals("Player")) && !owner.Equals(hit.collider.gameObject.tag))
            {
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(this.damage);
                Debug.Log("HeHeHeHa!");
            }
        }
        Destroy(this.gameObject);
    }
}

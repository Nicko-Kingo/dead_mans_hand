using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGrenade : BasicAttack
{
    
    

    public IEnumerator explode()
    {
        yield return new WaitForSecondsRealtime(.5f);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.gameObject.transform.position,1, new Vector3(0,0,0)); //Should only detect enemies
        
        foreach(RaycastHit2D hit in hits)
        {
            if((hit.collider.gameObject.tag == tag && hit.collider.gameObject.tag == "Enemy")
            ||
               tag == "Player" && hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(this.damage);
                Debug.Log("HeHeHeHA!");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : BasicAttack
{
    
    public void attack(Vector3 direction)
    {
        
        Debug.Log("Whip");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position + (direction - this.transform.position).normalized * .5f, 2, new Vector3(0,0,0));
            //Play the animation here

            foreach(RaycastHit2D hit in hits) 
            {
                Vector3 vectorToCollider = (hit.collider.transform.position - this.transform.position).normalized;  

                //Should be 180 degree arc
                if(Vector3.Dot(vectorToCollider, (direction - this.transform.position).normalized) > 0)
                {
                    //use .equals here
                    if((hit.collider.gameObject.tag == owner && hit.collider.gameObject.tag == "Enemy")
                        ||
                        owner == "Player" && hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Health>().TakeDamage(this.damage);
                        Debug.Log("Cheese");
                    }
                }
            }
        StartCoroutine(whip()); //little thingy to get rid of it after some time
    }



    private IEnumerator whip()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }
}

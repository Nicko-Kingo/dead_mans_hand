using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : BasicAttack
{

    public GameObject grenadelit;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Player")) && !owner.Equals(col.gameObject.tag))
        {
            explosion();
        }
    }


    public void explosion()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.gameObject.transform.position,2, new Vector3(0,0,0)); //Should only detect enemies
        
        foreach(RaycastHit2D hit in hits)
        {
            if((hit.collider.gameObject.tag == tag && hit.collider.gameObject.tag == "Enemy")
            ||
               tag == "Player" && hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(this.damage);
                Debug.Log("HHEHEHEHEHHEHEHEHBHEBEBHHEHEHEHEHEHEHHEHE");
            }
        }


        //this.gameObject.SetActive(false); //make it dissapear;

        //instantiate 5 smaller grenades in a pattern around it
        for(int i = 0; i < 5; i++)
        {

            //do some stupid ass trigonometry here
            //72 degrees around

            GameObject childGrenade = Instantiate(grenadelit, this.gameObject.transform.position, Quaternion.identity);
            LeanTween.move(childGrenade, this.gameObject.transform.position + (new Vector3(2 + Mathf.Cos(144 * i * i), 2 + Mathf.Sin(144 * i * i), 0)), 1f ).setOnComplete(() => {
                //make a child grenade dealy here
                
                Destroy(childGrenade);
            });
        }

        Destroy(this.transform.parent.gameObject);
    }

    public IEnumerator ClusterBomb()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        
        
        explosion();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : BasicAttack
{

    public GameObject grenadelit;

    private bool exploded = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Player")) && !owner.Equals(col.gameObject.tag))
        {
            explosion();
        }
    }


    public void explosion()
    {
        exploded = true;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.gameObject.transform.position,2, new Vector3(0,0,0)); //Should only detect enemies
        
        foreach(RaycastHit2D hit in hits)
        {
            if((hit.collider.gameObject.tag.Equals("Enemy") || hit.collider.gameObject.tag.Equals("Player")) && !owner.Equals(hit.collider.gameObject.tag))
            {
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(this.damage);
                Debug.Log("HHEHEHEHEHHEHEHEHBHEBEBHHEHEHEHEHEHEHHEHE");
            }
        }


        this.gameObject.SetActive(false); //make it dissapear;

        //instantiate 5 smaller grenades in a pattern around it
        for(int i = 0; i < 5; i++)
        {
            GameObject childGrenade = Instantiate(grenadelit, this.gameObject.transform.position, Quaternion.identity);
            LeanTween.move(childGrenade, this.gameObject.transform.position + (new Vector3(Mathf.Cos(288 * i), Mathf.Sin(288 * i), 0)), 1f ).setOnComplete(() => {
                //make a child grenade dealy here
                childGrenade.GetComponentInChildren<BabyGrenade>().explode();
            });
        }

        Destroy(this.transform.parent.gameObject);
    }

    public IEnumerator ClusterBomb()
    {
        if(exploded) StopCoroutine(ClusterBomb());
        yield return new WaitForSecondsRealtime(2.0f);
        
        
        this.explosion();
    }
}

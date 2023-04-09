using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{

    public GameObject bullet;
    public GameObject punch;

    public GameObject grenade;

    public GameObject grenadelit;
    
    public Camera cam;

    public void Attack(int handValue, Vector3 direction, string tag, bool isBlackjack)
    {
        if(handValue <= 0 || handValue > 21) return;
        else if(handValue <= 5)
        {
            

            //punch that only moves a short distance;
            //Change this to instantiating a short punch

            GameObject obj = Instantiate(punch, this.transform.position + (direction - this.transform.position).normalized * .5f, Quaternion.identity);
            obj.GetComponent<Projectiles>().owner = tag;

            //rotating object
            obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, Vector3.Angle(direction, this.transform.position));
            LeanTween.move(obj,this.transform.position + (direction - this.transform.position).normalized * 1f,.2f).setEaseInOutSine().setOnComplete(() => {
                
                Destroy(obj);
            });

            
            
        }
        else if(handValue <= 10)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
            newBullet.GetComponent<Projectiles>().owner = tag;
            LeanTween.move(newBullet, direction, .5f);

            //Note, use onComplete to do other stuff
        }
        else if(handValue <= 15)
        {
            //Some kind of whip attack
            //Modify the 2nd to last number to change the size of the attack
            RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position + (direction - this.transform.position).normalized * .5f, 2, new Vector3(0,0,0));
            //Play the animation here

            foreach(RaycastHit2D hit in hits) 
            {
                Vector3 vectorToCollider = (hit.collider.transform.position - this.transform.position).normalized;


                //Should be 180 degree arc
                if(Vector3.Dot(vectorToCollider, (direction - this.transform.position).normalized) > 0)
                {
                    if((hit.collider.gameObject.tag == tag && hit.collider.gameObject.tag == "Enemy")
                        ||
                        tag == "Player" && hit.collider.gameObject.tag == "Enemy")
                    {
                        Debug.Log("Cheese");
                    }
                }
            }
        }
        else if(handValue <= 20)
        {
            GameObject newGrenade = Instantiate(grenade, this.transform.position, Quaternion.identity);
            LeanTween.move(newGrenade, direction, 0.5f).setEaseInOutSine().setOnComplete(() => 
            {
                StartCoroutine(ClusterBomb(newGrenade, tag));
                
            });
        }
        else if(handValue == 21 && !isBlackjack)
        {

        }
        else if(isBlackjack)
        {

        }

        
    }

    public IEnumerator ClusterBomb(GameObject grenade, string tag)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        
        /*
        //Stop doing this??
        RaycastHit2D[] hits = Physics2D.CircleCastAll(grenade.transform.position,2, new Vector3(0,0,0)); //Should only detect enemies
        
        foreach(RaycastHit2D hit in hits)
        {
            if((hit.collider.gameObject.tag == tag && hit.collider.gameObject.tag == "Enemy")
            ||
               tag == "Player" && hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Cheese");
            }
        }
        */

        grenade.SetActive(false); //make it dissapear;

        //instantiate 5 smaller grenades in a pattern around it
        for(int i = 0; i < 1; i++)
        {

            //do some stupid ass trigonometry here

            GameObject childGrenade = Instantiate(grenadelit, grenade.transform.position, Quaternion.identity);
            LeanTween.move(childGrenade, grenade.transform.position + new Vector3(1,0,0), 0.2f ).setOnComplete(() => {
                Destroy(childGrenade);
            });
        }

        Destroy(grenade);

    }



}

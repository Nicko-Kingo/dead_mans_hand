using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{

    public GameObject bullet;
    public GameObject punch;

    public GameObject grenade;
    
    public GameObject whip;
    
    public GameObject laser;

    public GameObject screenWipe;

    public void Attack(int handValue, Vector3 direction, string tag, bool isBlackjack)
    {
        if(handValue <= 0 || handValue > 21) return;
        else if(handValue <= 5)
        {
            //punch that only moves a short distance;
            //Change this to instantiating a short punch
            direction.z = 0f;
            GameObject obj = Instantiate(punch, this.transform.position + (direction - this.transform.position).normalized * 1f, Quaternion.identity);
            obj.GetComponentInChildren<BasicAttack>().SetOwner(tag);

            //rotating object
            //obj.transform.RotateAround()
            obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, -Vector2.SignedAngle(direction - this.transform.position, Vector3.up));
            LeanTween.move(obj, obj.transform.position + (direction - this.transform.position).normalized * 1f,.2f).setEaseInOutSine().setOnComplete(() => {
                Destroy(obj);
            });

            
            
        }
        else if(handValue <= 10)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
            newBullet.GetComponent<BasicAttack>().SetOwner(tag);
            LeanTween.move(newBullet, direction, .4f).setOnComplete(() => {
                Destroy(newBullet);
            });

            //Note, use onComplete to do other stuff
        }
        else if(handValue <= 15)
        {
            //instantiate a whiparino here
            direction.z = 0f;
            GameObject newWhip = Instantiate(whip, this.transform.position + (direction - this.transform.position).normalized * 1f, Quaternion.identity);
            newWhip.GetComponentInChildren<BasicAttack>().SetOwner(tag);

            //rotating object
            //obj.transform.RotateAround()
            newWhip.transform.eulerAngles = new Vector3(newWhip.transform.eulerAngles.x, newWhip.transform.eulerAngles.y, -Vector2.SignedAngle(direction - this.transform.position, Vector3.up));

            newWhip.GetComponent<Whip>().attack(direction);


        }
        else if(handValue <= 20)
        {
            GameObject newGrenade = Instantiate(grenade, this.transform.position, Quaternion.identity);
            newGrenade.GetComponentInChildren<BasicAttack>().SetOwner(tag);
            LeanTween.move(newGrenade, direction + new Vector3(0,0,10), 0.5f).setEaseInOutSine().setOnComplete(() => 
            {
                StartCoroutine(newGrenade.GetComponentInChildren<Grenade>().ClusterBomb());
            });
        }
        /*
        else if(handValue == 21 && !isBlackjack)
        {
            GameObject newLaser = Instantiate(laser, this.transform.position + (direction - this.transform.position).normalized * 1f, Quaternion.identity);
            newLaser.GetComponentInChildren<BasicAttack>().SetOwner(tag);
            newLaser.GetComponentInChildren<Laser>().Shoot(direction);
        }
        */
        else if(handValue == 21 || isBlackjack)
        {
            Debug.Log("BlackJack");
            
            //Only player gets blackjack cause I said so
            if(tag.Equals("Enemy")) Destroy(this.gameObject);
            

            if(tag.Equals("Player"))
            {
                GameObject newScreenWipe = Instantiate(screenWipe, this.transform.position, Quaternion.identity);
                newScreenWipe.GetComponentInChildren<BasicAttack>().SetOwner(tag);
            }
        }

        
    }

    



}

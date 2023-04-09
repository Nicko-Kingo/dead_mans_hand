using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    [SerializeField] private int damage;

    public string owner;

    public int type;

    public GameObject grenadelit;

    public void OnTriggerEnter2D(Collider2D col) 
    {
        
        if(owner == "Player" && col.gameObject.tag == "Player") return;


        if((owner == "Player" && col.gameObject.tag == "Enemy") || 
        (owner == "Enemy" && (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")))
        {
            Debug.Log("HIT");

            switch(type)
            {
                case 0:
                    //Apply damage
                    break;
                case 1:
                    //do explosion
                    break;
                case 2:
                    clusterBomb(this.gameObject);
                    break;
                case 3:
                    //do laser attack feature
                    break;
                case 4:
                    //do blackjack attack
                    break;
            }

        }
        
        Destroy(this.gameObject);
    }


    private void clusterBomb(GameObject grenade)
    {
        grenade.SetActive(false); //make it dissapear;

        //instantiate 5 smaller grenades in a pattern around it
        for(int i = 0; i < 1; i++)
        {

            //do some stupid ass trigonometry here

            GameObject childGrenade = Instantiate(grenadelit, grenade.transform.position, Quaternion.identity);
            LeanTween.move(childGrenade, grenade.transform.position + new Vector3(1,0,0), 0.2f );
        }
    }
    


}

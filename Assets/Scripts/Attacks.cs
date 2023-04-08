using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{

    public GameObject bullet;
    public GameObject punch;

    // Start is called before the first frame update
    void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(int handValue, Vector3 direction)
    {
        if(handValue <= 0) return;
        if(handValue <= 5)
        {
            //punch that only moves a short distance;
            RaycastHit2D[] hits = Physics2D.BoxCastAll(this.transform.position, new Vector2(10,10), 360f, direction);
            Debug.DrawLine(this.transform.position, direction, Color.red, 10);

            foreach(RaycastHit2D hit in hits)
            {
                Debug.Log(hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "Enemy")
            {
                Destroy(hit.collider.gameObject);
            }
            }

            
            
        }
        if(handValue <= 10)
        {
            ;
        }
        else if(handValue <= 15)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
            LeanTween.move(newBullet, direction, .5f).setEaseInOutSine();
            //Note, use onComplete to do other stuff
        }
    }



}

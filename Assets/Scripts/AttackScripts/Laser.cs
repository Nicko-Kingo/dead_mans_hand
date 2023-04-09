using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BasicAttack
{
    public void Shoot(Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, direction, 20);

        foreach(RaycastHit2D hit in hits)
        {
            if((hit.collider.gameObject.tag == owner && hit.collider.gameObject.tag == "Enemy")
                    ||
                    owner == "Player" && hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("Cheese");
                }
        }

        Destroy(this.transform.parent.gameObject);
    }
}

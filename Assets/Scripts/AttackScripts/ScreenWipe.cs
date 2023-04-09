using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWipe : BasicAttack
{

    void Awake()
    {
        StartCoroutine(selfDestruct());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(
        "Here"
        );
        if(owner.Equals("Player") 
        && col.transform.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("THE INDIVIDUAL SHOULD BE DEAD");
            Destroy(col.gameObject); //??????
        }
    }


    private IEnumerator selfDestruct()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(this.gameObject);
    }
}

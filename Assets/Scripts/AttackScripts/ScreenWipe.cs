using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWipe : BasicAttack
{

    void Awake()
    {
        StartCoroutine(selfDestruct());
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(owner.Equals("Player") && col.gameObject.tag.Equals("Enemy"))
        {
            Destroy(col.transform.parent.gameObject); //??????
        }
    }


    private IEnumerator selfDestruct()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(this.gameObject);
    }
}

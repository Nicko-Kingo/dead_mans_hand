using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool wokenUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!wokenUp && other.name.Equals("WakeupCircle"))
        {
            this.GetComponent<EnemyBlackjack>().enabled = true;
            Debug.Log(this.name + " woke up!");
            wokenUp = true;
        }
    }


    public void TempAttack(int bjNum, Vector3 direction)
    {

    }
}

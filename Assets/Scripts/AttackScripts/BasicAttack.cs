using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [SerializeField] protected float damage;
    protected string owner;
    
    public void SetOwner(string newOwner)
    {
        owner = newOwner;
    }
}

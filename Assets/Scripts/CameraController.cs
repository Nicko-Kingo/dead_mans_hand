using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    

    public void Update()
    {
  
        transform.position = player.transform.position
         + new Vector3(0, 0, -15);
    }
}

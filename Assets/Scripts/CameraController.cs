using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    

    public void Update()
    {
        Vector3 Player = player.transform.position;
        Vector3 moose  = new Vector3(Input.mousePosition.x - (Screen.width/2),Input.mousePosition.y - (Screen.height/2));

        Vector3 newCam = new Vector3(0,0,0);

        newCam.x = moose.x + (Player.x - moose.x) / 10;
        newCam.y = moose.y + (Player.y - moose.y) / 10;


        transform.position = player.transform.position + (newCam / 100)         + new Vector3(0, 0, -15);
    }
}

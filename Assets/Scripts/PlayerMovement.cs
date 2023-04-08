using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float Speed = 5;
    private bool isGrounded;
    public Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x * Speed, vertical * Speed);

    }

    
}

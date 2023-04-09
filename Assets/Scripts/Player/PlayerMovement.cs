using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float Speed = 2500f;
    private bool isGrounded;
    public Rigidbody2D rb;
    private Vector2 newForce = Vector2.zero;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //rb.velocity = new Vector2(x * Speed, vertical * Speed);
        newForce.x = horizontal * Speed * Time.deltaTime;
        newForce.y = vertical * Speed * Time.deltaTime;
        rb.AddForce(newForce);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer playerSprite;
    public float idleDeadband = 1f;
    public GameObject explosion;

    private bool beingHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beingHurt) return;
        Vector3 vel = rb.velocity;
        if (vel.magnitude > idleDeadband)
        {
            anim.Play("PlayerWalk");
        }
        else
        {
            anim.Play("PlayerIdle");
        }
        if (vel.x > idleDeadband)
        {
            playerSprite.flipX = false;
        }
        else if (vel.x < -idleDeadband)
        {
            playerSprite.flipX = true;
        }

        // Temp stuff
        if (Input.GetKeyDown(KeyCode.Z)) // Temp hurt key
        {
            StartCoroutine(GetHurt());
        }
        else if (Input.GetKeyDown(KeyCode.X)) // Temp die key
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator GetHurt()
    {
        beingHurt = true;
        anim.Play("Hurt");
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        beingHurt = false;
    }

    private IEnumerator Die()
    {
        beingHurt = true;
        anim.Play("Hurt");
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        anim.Play("Death");
        explosion.SetActive(true);
        yield return new WaitForSeconds(.9f);
        explosion.SetActive(false);
    }
}

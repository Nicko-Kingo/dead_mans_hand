using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer enemySprite;
    public float idleDeadband = 1f;
    public GameObject explosion;

    private bool beingHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingHurt) return;
        Vector3 vel = rb.velocity;
        anim.Play("EnemyIdle");
        if (vel.x > idleDeadband)
        {
            enemySprite.flipX = true;
        }
        else if (vel.x < -idleDeadband)
        {
            enemySprite.flipX = false;
        }

/*         // Temp stuff
        if (Input.GetKeyDown(KeyCode.Z)) // Temp hurt key
        {
            StartCoroutine(GetHurt());
        }
        else if (Input.GetKeyDown(KeyCode.X)) // Temp die key
        {
            StartCoroutine(Die());
        } */
    }

    public void PlayGetHurt()
    {
        StartCoroutine(GetHurt());
    }

    public void PlayDeath()
    {
        StartCoroutine(Die());
    }

    private IEnumerator GetHurt()
    {
        beingHurt = true;
        anim.Play("EnemyHurt");
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        beingHurt = false;
    }

    private IEnumerator Die()
    {
        beingHurt = true;
        // explode here or something idk
        yield return null;
        /* anim.Play("Hurt");
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        anim.Play("Death");
        explosion.SetActive(true);
        yield return new WaitForSeconds(.9f);
        explosion.SetActive(false); */
    }
}

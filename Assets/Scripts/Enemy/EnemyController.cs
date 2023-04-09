using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static float defaultHoverDistance = 4f;
    public bool wokenUp = false;

    private Vector2 playerPosition = new Vector2(Screen.width / 2, Screen.height / 2);
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private float hoverDistance = 4f;
    public float attackDistance = 1f;
    public float enemySpeed = 100f;
    public float moveDeadband = 50f;
    private bool atDesiredPosition = false;
    private bool playAgain = false;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(this.transform.position);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!wokenUp) return;
        Vector2 difference = (Vector2)player.transform.position - (Vector2)this.transform.position;

        moveDirection = difference - difference.normalized * hoverDistance;
        if (moveDirection.sqrMagnitude > moveDeadband)
        {
            //rb.velocity = moveDirection.normalized * enemySpeed;
            rb.AddForce(moveDirection.normalized * enemySpeed * Time.deltaTime);
            atDesiredPosition = false;
        }
        else
        {
            atDesiredPosition = true;
        }

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


    public void Attack(int bjNum, Vector3 direction, bool trueBlackjack)
    {
        if ((bjNum <= 15 && bjNum >= 11) || bjNum <= 5)
        {
            StartCoroutine(GetInRange(bjNum, direction, trueBlackjack));
        }
        else
        {
            GetComponent<Attacks>().Attack(bjNum, player.transform.position, "Enemy", trueBlackjack);
            playAgain = true;
        }
    }


    public IEnumerator GetInRange(int bjNum, Vector3 direction, bool trueBlackjack)
    {
        hoverDistance = attackDistance;
        yield return new WaitForSeconds(.5f);
        while (!atDesiredPosition)
        {
            yield return new WaitForSeconds(.1f);
        }
        //Do some attack
        Debug.Log("Attacking");
        yield return new WaitForSeconds(1f);
        GetComponent<Attacks>().Attack(bjNum, player.transform.position, "Enemy", trueBlackjack);
        hoverDistance = defaultHoverDistance;
        playAgain = true;
    }

    public bool PlayAgain()
    {
        if (playAgain)
        {
            playAgain = false;
            return true;
        }
        return playAgain;
    }

    public void StartDeath()
    {
        Destroy(this.gameObject);
    }

    public void GotHurt()
    {

    }
}

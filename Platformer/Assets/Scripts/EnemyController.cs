using UnityEngine;
using System.Collections;



public class EnemyControllers : MonoBehaviour
{

    public float speed;
    public bool startRight;

    Rigidbody2D rb;
    Animator anim;

    bool dead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dead = false;

        if (!startRight)
            speed *= -1;
    }

    void FixedUpdate()
    {
        if (dead)
            return;
        UpdateDirection();
        UpdateMovement();
    }

    void UpdateDirection()
    {
        if (speed != 0)
            transform.localScale = new Vector3(Mathf.Sign(speed), transform.localScale.y, transform.localScale.z);
    }

    void UpdateMovement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            speed *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrier"))
            speed *= -1;
    }

    public void Die()
    {
        anim.SetTrigger("Death");
        StartCoroutine(DelayedDeath());
    }

    IEnumerator DelayedDeath()
    {
        dead = true;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}


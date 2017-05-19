using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    
    Rigidbody2D rb;
    Animator anim;
    AudioSource audio;

    public Transform topLeft;
    public Transform bottomRight;

   
    public float speed;
    public float jump;
    public LayerMask ground;
    public bool dead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        
    }

    void FixedUpdate()
    {
        if (dead)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        UpdateMovement(h, v);
    }

    void UpdateMovement(float h, float v)
    {
        
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
        if (h != 0) transform.localScale = new Vector3(Mathf.Sign(h), transform.localScale.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));


        bool isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, ground);
        anim.SetBool("Jumping", !isGrounded);
        if (isGrounded == false)
        {
           
        }
           

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            audio.Play();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            BoxCollider2D enemyCollider = other.gameObject.GetComponent<BoxCollider2D>();

            float enemyTop = other.transform.position.y + (enemyCollider.size.y / 2) + enemyCollider.offset.y;

            if (transform.position.y > enemyTop)
            {
                other.gameObject.GetComponent<EnemyController>().Die();

                rb.velocity = new Vector2(rb.velocity.x, jump * 0.5f);
            }
            else
            {
                Die();
            }
        }
        else if (other.gameObject.CompareTag("LevelEnd"))

    {
            
            SceneManager.LoadScene(Application.loadedLevel + 1);
           
        }
    }

    public void Die()
    {
        StartCoroutine(DelayedDeath());
    }

    IEnumerator DelayedDeath()
    {
        rb.velocity = Vector2.zero;
        dead = true;
        anim.SetBool("Jumping", false);
        anim.SetFloat("Speed", 0);

        yield return new WaitForSeconds(0.1f);

        GetComponent<BoxCollider2D>().enabled = false;
        rb.AddForce(Vector2.up * 1000);

        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.9f);

        
        Application.LoadLevel(Application.loadedLevel);

    }



}


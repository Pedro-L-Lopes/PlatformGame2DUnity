using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int life;
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sprite;


    private Vector2 direction;
    private bool isGrounded;
    private bool recovery;

    void Start()
    {

    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Jump();
        PlayAmim();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rig.velocity = new Vector2(direction.x * speed, rig.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            anim.SetInteger("transition", 2);
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Death()
    {

    }

    void PlayAmim()
    {
        if (direction.x > 0)
        {
            if (isGrounded)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction.x < 0)
        {
            if (isGrounded)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (direction.x == 0)
        {
            if (isGrounded)
            {
                anim.SetInteger("transition", 0);
            }
        }
    }

    public void Hit()
    {

        if (recovery == false)
        {
            StartCoroutine(Flick());
        }
    }

    IEnumerator Flick()
    {
        recovery = true;
        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 1);
        life -= 1;
        recovery = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator animator;
    private SpriteRenderer sprite;
        
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float movespeed = 7f;
    [SerializeField] private float jumpforce = 10f;

    private enum PlayerState { idle, running, jumping, falling, death}

    public ItemCollector collector;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpforce, 0);
        }

        AnimationUpdate();

    }

    private void AnimationUpdate()
    {
        PlayerState state;

        if (dirX > 0f)
        {
            state = PlayerState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = PlayerState.running;
            sprite.flipX = true;
        }
        else
        {
            state = PlayerState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = PlayerState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = PlayerState.falling;
        }

        animator.SetInteger("State", (int)state);
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruit")) 
        {
            collector.fruits++;
            Destroy(other.gameObject);
        }
    }
}

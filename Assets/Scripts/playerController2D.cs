using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Next thing to work on is player jump behavior. Get groundCheck to work.
//https://www.youtube.com/watch?v=44djqUTg2Sg
//30ish minutes in
public class playerController2D : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    public AudioSource fruitGet;

    bool isGrounded;
    bool isOnWallLeft;
    bool isOnWallRight;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;
    [SerializeField]
    Transform wallLeft;
    [SerializeField]
    Transform wallRight;

    private float runSpeed = 6f;
    // Start is called before the first frame update
    void Start () {
        animator = GetComponent<Animator> ();
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void FixedUpdate () {
        //Checks if the player is on the ground
        if ((Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"))) ||
           (Physics2D.Linecast (transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer ("Ground"))) ||
           (Physics2D.Linecast (transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer ("Ground")))) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        //Checks if the player is touching a wall on the left side
        if (Physics2D.Linecast (transform.position, wallLeft.position, 1 << LayerMask.NameToLayer ("Ground")) && !isGrounded) {
            isOnWallLeft = true;
        } else {
            isOnWallLeft = false;
        }
        //Checks if the player is touching a wall on the right side
        if (Physics2D.Linecast (transform.position, wallRight.position, 1 << LayerMask.NameToLayer ("Ground")) && !isGrounded) {
            isOnWallRight = true;
        } else {
            isOnWallRight = false;
        }

        if ((Input.GetKey ("d") || Input.GetKey ("right")) && !isOnWallRight && isGrounded) {
            rb2d.velocity = new Vector2 (rb2d.velocity.x + 0.5f, rb2d.velocity.y);
            if (rb2d.velocity.x > 6f)
            {
                rb2d.velocity = new Vector2 (6f, rb2d.velocity.y);
            }
            if (isGrounded == true) {
                animator.Play ("playerRun");
            }
            spriteRenderer.flipX = false;
        } else if ((Input.GetKey ("a") || Input.GetKey ("left")) && !isOnWallLeft && isGrounded) {
            rb2d.velocity = new Vector2 (rb2d.velocity.x - 0.5f, rb2d.velocity.y);
            if (rb2d.velocity.x < -6f)
            {
                rb2d.velocity = new Vector2 (-6f, rb2d.velocity.y);
            }
            if (isGrounded == true) {
                animator.Play ("playerRun");
            }
            spriteRenderer.flipX = true;
        } else {
            if (isGrounded == true) {
                animator.Play ("playerIdle");
                rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
            }
        }

        if (Input.GetKey ("space") && isGrounded) {
            rb2d.velocity = new Vector2 (rb2d.velocity.x, 25);
            animator.Play ("playerJump");
        }

        if ((!isGrounded && rb2d.velocity.y > 0) && !isOnWallLeft && !isOnWallRight) {
            animator.Play ("playerJump");
        }

        if ((!isGrounded && rb2d.velocity.y < 0) && !isOnWallLeft && !isOnWallRight) {
            animator.Play ("playerFall");
        }

        if (isOnWallLeft && rb2d.velocity.y < 0) {
            rb2d.velocity = new Vector2 (rb2d.velocity.x, (rb2d.velocity.y / 2));
            animator.Play ("playerWallJump");
            spriteRenderer.flipX = true;
            if (Input.GetKey ("space")) {
                rb2d.velocity = new Vector2 (rb2d.velocity.x + runSpeed, 25);
                spriteRenderer.flipX = false;
            }
        }

        if (isOnWallRight && rb2d.velocity.y < 0) {
            rb2d.velocity = new Vector2 (rb2d.velocity.x, (rb2d.velocity.y / 2));
            animator.Play ("playerWallJump");
            spriteRenderer.flipX = false;
            if (Input.GetKey ("space")) {
                rb2d.velocity = new Vector2 (rb2d.velocity.x - runSpeed, 25);
                spriteRenderer.flipX = true;
            }
        }

        if (rb2d.velocity.y <= -15) {
            rb2d.velocity = new Vector2 (rb2d.velocity.x, -15);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);
            fruitGet.Play();
            ScoreScript.scoreValue += 100;
        }
    }

}
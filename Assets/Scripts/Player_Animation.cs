using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirx;
    [SerializeField] private float Jumpforce = 14f;
    [SerializeField] private float Moveforce = 7f;
    [SerializeField] private bool IsGrounded;
    [SerializeField] private Transform SpawnPoint;
    Transform SelfTransform;


    private enum MovementState { idel, running, jumping, falling, crouching, attacking, slide }


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        SelfTransform = transform;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxis("Horizontal");
        rbody.velocity = new Vector2(dirx * Moveforce, rbody.velocity.y);

        if (Input.GetKey(KeyCode.W) && IsGrounded)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, Jumpforce);
        }
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        //Animation
        MovementState state;
        if (dirx > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idel;
        }

        if (rbody.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rbody.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        if (Input.GetKey(KeyCode.S))
        {
            state = MovementState.crouching;
        }

        if (Input.GetKey(KeyCode.E))
        {
            state = MovementState.attacking;
        }


        anim.SetInteger("state", (int)state);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagTracker.groundTag)
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagTracker.groundTag)
        {
            IsGrounded = false;
        }
    }
    void SpawnPlayer()
    {
        SelfTransform.position = SpawnPoint.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagTracker.killTriggerTag)
        {
            SpawnPlayer();
        }
    }

}

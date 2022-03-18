using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float runSpeed;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    private Animator anim;
    
    private float Horizontalinput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    void FixedUpdate()
    {
         Horizontalinput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Horizontalinput * runSpeed, rb.velocity.y);
        
        //Flip the player left and right 

        if(Horizontalinput>0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (Horizontalinput < -0.01f) {
            transform.localScale = new Vector3(-1,1,1);

        }

        //Jump
        if (Input.GetKey(KeyCode.W) && IsGrounded())
        {
            Jump();

        }
       
        // Set Animation

        anim.SetBool("running", Horizontalinput != 0);
        anim.SetBool("ground", IsGrounded());
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, runSpeed);
        anim.SetTrigger("jump");
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f,groundLayer);
        return raycastHit.collider !=null;
    }
    public bool canAttack()
    {
        return Horizontalinput==0;
    }
}

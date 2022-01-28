//ICS4U0-A
//Bains, J
//Jayson Sarai, Ryan Hunter, Bhavkaran Mann, Zavere Vidal 

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; //Creating a speed variable 

    [SerializeField] private float jumpPower;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body; //Creating a reference to player RigidBody2D 

    private Animator anim;

    private BoxCollider2D boxCollider;

    private float WallJumpCooldown;

    private float horizontalInput;


    [Header("SFX")]

    [SerializeField] private AudioClip jumpSound;


    private void Awake() //Creating an awake method 
    {
        // Grabs references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();  

        anim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()  
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // flip player when moving left & right 
        if (horizontalInput > 0.01f)

            transform.localScale = Vector3.one;

        else if (horizontalInput < -0.01f)

            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters 
        anim.SetBool("Run", horizontalInput != 0);

        anim.SetBool("Grounded", isGrounded());

        // The wall jump logic
        if (WallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;

                body.velocity = Vector2.zero;
            }
            else

                body.gravityScale = 10;

            if (Input.GetKey(KeyCode.Space)) //Using a boolean variable to make the player jump 
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())

                    SoundManager.instance.PlaySound(jumpSound);
            }

        }
        else

            WallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); //When pressing space it will maintain the current velocity 

            anim.SetTrigger("Jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);

                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y,transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 30, 10);

            WallJumpCooldown = 0;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }


}
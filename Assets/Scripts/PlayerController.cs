using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 400f;

    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float checkRadius = 0.5f;
    [SerializeField]
    private LayerMask whatIsGround;


    [SerializeField]
    private int maxExtraJumps = 1;
    private int extraJumps;

    [SerializeField]
    private bool debugMovement = false;
    private float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = maxExtraJumps;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (debugMovement)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        } else
        {
            moveInput = 1;
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        if (isGrounded)
        {
            extraJumps = maxExtraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            } else if (extraJumps > 0)
            {
                Jump();
                extraJumps -= 1;
            }
        }



    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private int maxHealth = 3;
    private int currentHealth;

    [SerializeField]
    private float invulnerabiltySeconds = 2f;
    private bool isInvulnerable = false;

    #region player_movement

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 400f;

    [SerializeField]
    private int maxExtraJumps = 1;
    private int extraJumps = 0;
    private bool isJumping = false;

    private bool isGrounded = true;
    [SerializeField]
    private Transform groundCheck = null;
    [SerializeField]
    private float checkDist = 0.5f;
    [SerializeField]
    private LayerMask whatIsGround;


    [SerializeField]
    private bool debugMovement = false;
    private float moveInput;


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = maxExtraJumps;

        currentHealth = maxHealth;
        GameEvents.current.PlayerHealthChange(currentHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDist, whatIsGround);

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        if (debugMovement)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            moveInput = 1;
        }

        if (rb.velocity.y <= 0)
        {
            isJumping = false;
        }

        if (isGrounded & !isJumping)
        {
            extraJumps = maxExtraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }




        //For debug purposes
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(1);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (extraJumps > 0)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            extraJumps -= 1;
        }

    }
       
    public void TakeDamage(int amount)
    {
        if (!isInvulnerable)
        {
            currentHealth -= amount;
            //TODO add animations
            if (currentHealth <= 0)
            {
                Debug.Log("Died");
            }
            else
            {
                StartCoroutine("InvulnerabilityCo");
            }
            GameEvents.current.PlayerHealthChange(currentHealth);
        }
    }

    private IEnumerator InvulnerabilityCo()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabiltySeconds);
        isInvulnerable = false;
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
        }
        GameEvents.current.PlayerHealthChange(currentHealth);
    }
}

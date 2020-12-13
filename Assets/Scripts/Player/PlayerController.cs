﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private int maxHealth = 3;
    private int currentHealth;
    
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer SR;

    [SerializeField]
    private int score = 0;

    [SerializeField]
    private float invulnerabiltySeconds = 2f;
    private bool isInvulnerable = false;

    private float fallThreshold = -7f;

    private Vector2 lastCheckpoint;

    #region player_movement

    [SerializeField]
    private float speed = 10f;
    private float speedMultiplier = 0;
    [SerializeField]
    private float jumpForce = 400f;

    [SerializeField]
    private int maxExtraJumps = 1;
    private int extraJumps = 0;
    private bool isJumping = false;
    [SerializeField]
    private float maxTimeJump = 0.5f;
    private float timerJump = 0f;

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
        timerJump = 0f;

        currentHealth = maxHealth;
        GameEvents.current.PlayerHealthChange(currentHealth);

        GameEvents.current.PlayerScoreChange(score);

        GameEvents.current.onCheckpointReached += RegisterCheckpoint;
        lastCheckpoint = gameObject.transform.position;

        GameEvents.current.ChangeSpeedMultiplier(speedMultiplier);

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

        if (moveInput != 0)
        {
            ChangeSpeedMultiplier(moveInput);
            if (moveInput > 0.001)
            {
                SR.flipX = false;
                animator.SetBool("IsRunning", true);
            }

            else if (moveInput < -0.001)
            {
                SR.flipX = true;
                animator.SetBool("IsRunning", true);
            }


        }
        else
        {
            ChangeSpeedMultiplier(0);
            animator.SetBool("IsRunning", false);
        }

        if (isGrounded == true)
        {
            animator.SetBool("IsJump", false);
        }
        else animator.SetBool("IsJump", true);



        if (transform.position.y < fallThreshold)
        {
            HandleFall();
        }
        else
        {
            HandleJumping();
        }

    }

    private void ChangeSpeedMultiplier(float newSpeedMultiplier)
    {
        speedMultiplier = newSpeedMultiplier;
        GameEvents.current.ChangeSpeedMultiplier(newSpeedMultiplier);
    }

    private void HandleJumping()
    {
        if (rb.velocity.y <= 0)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping && rb.velocity.y == 0f)
        {
            extraJumps = maxExtraJumps;
            timerJump = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isJumping = true;
            } else
            {
                if (extraJumps > 0)
                {
                    isJumping = true;
                    extraJumps -= 1;
                    timerJump = 0f;
                }
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void Jump()
    {
        if (isJumping)
        {
            if (timerJump < maxTimeJump)
            {
                timerJump += Time.deltaTime;
                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                isJumping = false;
            }
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
                Die();
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
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
        GameEvents.current.PlayerHealthChange(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Died");
    }

    private void HandleFall()
    {
        TakeDamage(1);
        Respawn();
    }

    private void RegisterCheckpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint.position;
    }

    private void Respawn()
    {
        transform.position = lastCheckpoint;
    }

    private void OnDestroy()
    {
        GameEvents.current.onCheckpointReached -= RegisterCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ScoreGem")
        {
            score += other.gameObject.GetComponent<Gem>().GetValue();
            GameEvents.current.PlayerScoreChange(score);
        } else if (other.gameObject.tag == "HealingGem")
        {
            Heal(other.gameObject.GetComponent<Gem>().GetValue());
        }
    }
}
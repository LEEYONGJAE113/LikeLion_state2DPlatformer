using UnityEngine;

public class Player : Entity
{
    #region Jump
    [Header ("점프 정보")]
    [SerializeField]
    private float jumpForce;
    #endregion

    #region Movement
    [Header ("이동 정보")]
    [SerializeField]
    private float moveSpeed;
    private float xInput;
    #endregion

    #region Dash
    [Header("대쉬 정보")]
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashTime;
    [SerializeField]
    private float dashCooldown;
    [SerializeField]
    private float dashCooldownTimer;
    #endregion
    
    #region Attack
    [Header("공격 정보")]
    [SerializeField]
    private float comboTime;
    [SerializeField]
    private float comboTimeCounter;
    private bool isAttacking;
    private int comboCount;
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        CheckInput();

        dashTime -= Time.deltaTime;
        // dashCooldownTimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;

        FlipController();
        Movement();
        AnimatorControllers();
    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCount++;

        if (comboCount >= 3)
        {
            comboCount = 0;
        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attacking();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void Attacking()
    {
        if (!isGrounded) { return; }

        if (comboTimeCounter < 0)
        {
            comboCount = 0;
        }
        isAttacking = true;
        comboTimeCounter = comboTime;
    }

    private void Dash()
    {
        if(dashTime<= -dashCooldown /*dashCooldownTimer <= 0*/ && !isAttacking)
        {
            // dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }
    private void Movement()
    {
        if(isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
        }
    }

    private void Jump()
    {
        if (!isGrounded) { return; }
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.linearVelocityX != 0;

        anim.SetFloat("yVelocity", rb.linearVelocityY);

        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsDashing", dashTime > 0);
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetInteger("ComboCount", comboCount);
    }

    private void FlipController()
    {
        if((rb.linearVelocityX > 0 && !facingRight) || (rb.linearVelocityX < 0 && facingRight))
        {
            Flip();
        }
    }

}
